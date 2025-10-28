
using Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Contracts;
using Persistance.Data.DataSeed;
using Persistance.Repositories;
using AutoMapper;
using Services;
using Services.Abstractions;
using E_Commerce.CustomMiddleWares;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using E_Commerce.Factories;
using E_Commerce.Extensions;
using StackExchange.Redis;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

          

            //----------------------------------------------------------------------------------------
            #region Configure Services
            builder. Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddCoreServices();
            builder.Services.AddPresentationServices();
            builder.Services.AddTransient<PictureUrlResolver>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));
            });
            #endregion
            //------------------------------------------------------------------------------------
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Build
            var app = builder.Build();
            #endregion

            #region Middlewares
            app.UseCustomMiddleWareExceptions();
            await app.SeedDbAsync();
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
            #endregion





        }
    }
}
