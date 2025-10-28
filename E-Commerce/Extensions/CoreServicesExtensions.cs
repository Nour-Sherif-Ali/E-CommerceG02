using Services.Abstractions;
using Services;

namespace E_Commerce.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services) {
            Services.AddAutoMapper(X => X.AddProfile(new MappingProfiles()));
            Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }
    }
}
