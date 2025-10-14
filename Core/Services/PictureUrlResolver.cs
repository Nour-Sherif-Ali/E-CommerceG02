using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Shared.DTOS;
namespace Services
{
    public class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductDto, string>
    {
        //https://localhost:7229/{src.PictureUrl}
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                var Url = $"{configuration.GetSection("URLS")["BaseURL"]}{source.PictureUrl}";
                return Url;
            }
        }
    }
}
