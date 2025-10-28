using Services.Abstractions;
using Services;
using E_Commerce.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Extensions
{
    public  static class PresentationServicesExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection Services)
        {
            Services.AddControllers();
            Services.Configure<ApiBehaviorOptions>((options) =>  //ApiBehaviorOptions => Controls the API Behavior when there is an error in the Data
            {
                options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateApiValidationErrorResponse;
            });
            return Services;
        }
    }
}
