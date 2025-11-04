using Services.Abstractions;
using Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace E_Commerce.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(X => X.AddProfile(new MappingProfiles()));
            Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }
        public static IServiceCollection AddJWTService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTOptions:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWTOptions:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTOptions:SecretKey"]))

                };

            });

            return services;

        }

    }

}
    