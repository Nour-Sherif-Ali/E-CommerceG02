using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using Domain.Entities.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DTOS.OrderDtos;

namespace Services
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemsDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemsDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return string.Empty;
            }
            var Url = $"{_configuration.GetSection("URLS")["BaseURL"]}{source.Product.PictureUrl}";
            return Url;
        }
    }
}
