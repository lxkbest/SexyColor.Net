using System;
using System.Text;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SexyColor.CommonComponents;
using SexyColor.Infrastructure;
using SexyColor.Api.Jwt;
using log4net;
using log4net.Repository;
using SexyColor.Api.Business.v1_0_0;
using SexyColor.Api.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using System.Threading.Tasks;

namespace SexyColor.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            HostingEnvironment = env;
            repository = LogManager.CreateRepository("APISexyColor");
        }

        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }
        public IHostingEnvironment HostingEnvironment { get; }
        public ILoggerRepository repository { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.


        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            GlobalHostingEnvironment.HostingEnvironment = this.HostingEnvironment;
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());


            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(WebApiResultMiddleware));
                options.RespectBrowserAcceptHeader = true;

            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new LowercaseContractResolver();
            });

            var assembly = this.GetType().GetTypeInfo().Assembly;
            var builder = new ContainerBuilder();
            var manager = new ApplicationPartManager();
            var feature = new ControllerFeature();

            manager.ApplicationParts.Add(new AssemblyPart(assembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            manager.PopulateFeature(feature);

            builder.RegisterTypes(feature.Controllers.Select(controller => controller.AsType()).ToArray()).PropertiesAutowired();
            builder.Register(c => new DbService(Configuration.GetSection("ConnectionStrings")["DefaultConnection"])).AsSelf().PropertiesAutowired();
            string[] assembliesName = { "SexyColor.BusinessComponents", "SexyColor.CommonComponents" };
            var assemblies = assembliesName.Select(w => Assembly.Load(new AssemblyName(w))).ToArray();

            builder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("Repository")).AsSelf().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("Service")).AsSelf().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<SitesMiddlewareBusiness>().AsSelf().SingleInstance();
            builder.RegisterType<CategoryMiddlewareBusiness>().AsSelf().SingleInstance();


            builder.Register(c => new AuthorizerHelper()).AsSelf().SingleInstance().PropertiesAutowired();
            builder.Register(c => new DefaultIdGenerator()).As<IdGenerator>().SingleInstance();
            builder.Register(c => new DefaultUserIdToUserNameDictionary()).As<UserIdToUserNameDictionary>().SingleInstance().PropertiesAutowired();
            builder.Register(c => new DefaultCaptchaManager()).As<ICaptchaManager>().SingleInstance();
            builder.Register(c => new DefaultCacheService(new RedisMemoryCache(), new RuntimeMemoryCache(), 1.0F, true)).As<ICacheService>().SingleInstance();
            //builder.Register(c => new DefaultCacheService(new RuntimeMemoryCache(), 1.0F)).As<ICacheService>().SingleInstance();
            builder.Register(c => new DefaultStorageService()).As<IStorageService>().SingleInstance();
            builder.Register(c => new DefaultStoreProvider(@"~/images")).As<IStoreProvider>().SingleInstance();
            builder.Register(c => new Logger(this.HostingEnvironment)).As<SexyColor.CommonComponents.ILogger>().SingleInstance();
            builder.Register(c => new Log4NetLoggerFactoryAdapter(repository.Name)).As<ILoggerFactoryAdapter>().SingleInstance();
            builder.Register(c => new QuartzTaskScheduler()).As<ITaskScheduler>().SingleInstance();
            //泛型注册
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).SingleInstance().PropertiesAutowired();

            builder.Populate(services);
            this.ApplicationContainer = builder.Build();
            BuilderContext.Register(services, Configuration);
            ReadConfigurationHelper.Configuration = Configuration;
            DIContainer.RegisterContainer(this.ApplicationContainer);
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider svp)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            HttpContextCore.ServiceProvider = svp;

            var secretKey = "secretkey$%^#$%$%&*!@#sexycolor";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = "SexyColorWabAPI",
                ValidateAudience = true,
                ValidAudience = "SexyColorWabAPI",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            Func<StatusCodeContext, Task> handler = async context =>
            {
                var response = context.HttpContext.Response;
                if (response.StatusCode == 401)
                {
                    response.ContentType = "application/json";
                    await response.WriteAsync(JsonConvert.SerializeObject(new { code = 401, msg = "Unauthorized", result = "" }, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                }
                if (response.StatusCode == 404)
                {
                    response.ContentType = "application/json";
                    await response.WriteAsync(JsonConvert.SerializeObject(new { code = 404, msg = "Not Found", result = "" }, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                }
                if (response.StatusCode == 500)
                {
                    response.ContentType = "application/json";
                    await response.WriteAsync(JsonConvert.SerializeObject(new { code = 500, msg = "Server Eerror", result = "" }, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                }
            };
            app.UseStatusCodePages(handler);

            //在Header中传递
            //任何[Authorize]的请求都有需要有效的jwt
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters,
                BackchannelHttpHandler = new CustomJwtMessageHandler(),
                BackchannelTimeout = TimeSpan.Zero,
                IncludeErrorDetails = true,

            });

            var options = new TokenProviderOptions
            {
                Audience = "SexyColorWabAPI",
                Issuer = "SexyColorWabAPI",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Path = "/api/token"
            };
            app.UseMvc();
            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));
            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 404)
            //    {
            //        context.Response.ContentType = "application/json";
            //        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { code = 404, msg = "Not Found", result = "" }, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            //    }
            //});

            //在Cookie中传递
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true,
            //    AuthenticationScheme = "Cookie",
            //    CookieName = "access_token",
            //    //需要自定义实现 ISecureDataFormat接口的类，因为asp.net core cookie认证不支持传递jwt
            //    TicketDataFormat = new CustomJwtDataFormat(SecurityAlgorithms.HmacSha256, tokenValidationParameters)
            //});




        }
    }
}
