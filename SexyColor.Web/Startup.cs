using System;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using SexyColor.CommonComponents;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using log4net;
using log4net.Repository;

namespace SexyColor.Web
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
            if (env.IsDevelopment())
                builder.AddApplicationInsightsSettings(developerMode: true);
            HostingEnvironment = env;
            Configuration = builder.Build();
            repository = LogManager.CreateRepository("WebSexyColor");
        }
        public IHostingEnvironment HostingEnvironment { get; }
        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }
        public ILoggerRepository repository { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            GlobalHostingEnvironment.HostingEnvironment = this.HostingEnvironment;
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Add framework services.
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            services.AddAuthorization(authorization =>
            {
                authorization.AddPolicy("EnterSystem", policy => policy.Requirements.Add(new EnterSystemRequirement()));
            });
            services.AddSession(session =>
            {
                session.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddTimedJob();
            services.AddMvc(option =>
            {
                option.Filters.Add(new HandleErrorToLogAttribute());
            }).AddJsonOptions(option => {
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
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


            //仓储批量注册
            builder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("Repository")).AsSelf().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //服务批量注册
            builder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("Service")).AsSelf().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //业务组件注册
            builder.Register(c => new AuthorizerHelper()).AsSelf().SingleInstance().PropertiesAutowired();
            builder.Register(c => new DefaultIdGenerator()).As<IdGenerator>().SingleInstance();
            builder.Register(c => new DefaultUserIdToUserNameDictionary()).As<UserIdToUserNameDictionary>().SingleInstance().PropertiesAutowired();
            builder.Register(c => new DefaultCaptchaManager()).As<ICaptchaManager>().SingleInstance();
            //builder.Register(c => new DefaultCacheService(new RedisMemoryCache(), new RuntimeMemoryCache(), 1.0F, true)).As<ICacheService>().SingleInstance();
            builder.Register(c => new DefaultCacheService(new RuntimeMemoryCache(), 1.0F)).As<ICacheService>().SingleInstance();
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

            LogoSettings.RegisterSettings(LogoConfigManager.Instance().GetAllLogoConfigs());
            new DefaultResourceManager().Excute();
            ReadConfigurationHelper.Configuration = Configuration;
            DIContainer.RegisterContainer(this.ApplicationContainer);
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime, IServiceProvider svp)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            HttpContextCore.ServiceProvider = svp;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            string urlSuffix = string.Empty;
            var setting = Configuration["UseSuffix"];
            if (setting.Equals("true", StringComparison.CurrentCultureIgnoreCase))
            {
                urlSuffix = ".aspx";
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseTimedJob();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Sexy.Cookie",
                AutomaticAuthenticate = true,
                LoginPath = new PathString("/System/ManageLogin" + urlSuffix),
                AccessDeniedPath = new PathString("/Error/GlobalError" + urlSuffix),
                AutomaticChallenge = true,
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(30),
                SlidingExpiration = true,
                ClaimsIssuer = "http://www.sexy.com",
                CookiePath = "/"
            });
            TaskSchedulerFactory.GetScheduler().Start();
            app.UseMvc(routes =>
            {

                //routes.MapRoute(
                //   name: "Route",
                //   template: "{*path}",
                //   defaults: new { controller = "Manage", action = "Login" } );

                routes.MapRoute(
                   name: "Error",
                   template: "Error/{action}" + urlSuffix,
                   defaults: new { controller = "Error" });

                routes.MapRoute(
                    name: "System",
                    template: "System/{action}" + urlSuffix,
                    defaults: new { controller = "System", action = "ManageLogin" });

                routes.MapRoute(
                    name: "Default",
                    template: "",
                    defaults: new { controller = "System", action = "ManageLogin" });

                routes.MapRoute(
                    name: "SystemUser",
                    template: "System/User/{action}" + urlSuffix,
                    defaults: new { controller = "SystemUser", action = "ManageUser" });

                routes.MapRoute(
                    name: "SystemPermissions",
                    template: "System/Permissions/{action}" + urlSuffix,
                    defaults: new { controller = "SystemPermissions", action = "ManagePermission" });

                routes.MapRoute(
                   name: "SystemGoods",
                   template: "System/Goods/{action}" + urlSuffix,
                   defaults: new { controller = "SystemGoods", action = "ManagerGoods" });

                routes.MapRoute(
                    name: "SystemBasics",
                    template: "System/Basics/{action}" + urlSuffix,
                    defaults: new { controller = "SystemBasics", action = "ManageOperation" });

                routes.MapRoute(
                    name: "SystemSettings",
                    template: "System/Settings/{action}" + urlSuffix,
                    defaults: new { controller = "SystemSettings", action = "SiteSettings" });

                routes.MapRoute(
                    name: "SystemPoint",
                    template: "System/Point/{action}" + urlSuffix,
                    defaults: new { controller = "SystemPoint", action = "ManagerPoint" });

                routes.MapRoute(
                    name: "SystemOrder",
                    template: "System/Order/{action}" + urlSuffix,
                    defaults: new { controller = "SystemOrder", action = "ManagerOrder" });

            });
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
    }
}
