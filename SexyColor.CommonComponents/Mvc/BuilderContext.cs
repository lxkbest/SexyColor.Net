using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SexyColor.CommonComponents
{
    public static class BuilderContext
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static IServiceCollection ServiceCollection { get; set; }
        public static void Register(this IServiceCollection services, IConfigurationRoot configuration)
        {
            Configuration = configuration;
            ServiceCollection = services;
        }
    }
}