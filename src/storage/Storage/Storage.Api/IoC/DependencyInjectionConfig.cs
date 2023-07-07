using Storage.Api.Client;
using Storage.Api.Service;

namespace Storage.Api.IoC
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<StorageService>();
            services.AddScoped<IProductClient, ProductClient>();
        }
    }
}
