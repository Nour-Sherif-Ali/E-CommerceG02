
using Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Contracts;
using Persistance.Data.DataSeed;
using Persistance.Repositories;
using AutoMapper;
using Services;
using Services.Abstractions;
using E_Commerce.CustomMiddleWares;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            //----------------------------------------------------------------------------------------
            #region Configure Services
            builder.Services.AddDbContext<StoreDbContext>(options =>

            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(X => X.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddTransient<PictureUrlResolver>();
            #endregion
            //------------------------------------------------------------------------------------
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

           
            var app = builder.Build();
            #region Services
            var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>(); 
            await ObjectOfDataSeeding.DataSeedAsync();
            #endregion
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); //3lashan ne3raf nesha8al el images

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
