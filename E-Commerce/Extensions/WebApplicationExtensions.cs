using System.Runtime.CompilerServices;
using Domain.Contracts;
using E_Commerce.CustomMiddleWares;

namespace E_Commerce.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            #region Services
            var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();
            return app;
            #endregion
        }

        public static WebApplication UseCustomMiddleWareExceptions(this WebApplication app) 
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }
    }
}
