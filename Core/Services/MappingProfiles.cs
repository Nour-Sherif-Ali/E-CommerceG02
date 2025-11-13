using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.BasketModule;
using Domain.Entities.IdentityModule;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Shared.DTOS;
using Shared.DTOS.BasketDtos;
using Shared.DTOS.IdentityDto;
using Shared.DTOS.OrderDtos;

namespace Services
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Product
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.ProductType.Name))
                .ForMember(dist => dist.PictureUrl, options => options.MapFrom<PictureUrlResolver>());
            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();



            #endregion
            #region BasketItem
            CreateMap<CustomerBasket,BasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDto>().ReverseMap();
            #endregion
            #region Identity
            CreateMap<Address,AddressDto>().ReverseMap();
            #endregion

            #region Order
            CreateMap<ShippingAddressDto, ShippingAddress>().ReverseMap();

            
            CreateMap<OrderItem, OrderItemsDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>())
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity));

            
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, o => o.MapFrom(s => s.GetTotal()))
                .ForMember(d => d.Items, o => o.MapFrom(s => s.Items)) 
                .ForMember(d => d.SubTotal, o => o.MapFrom(s => s.Subtotal));

            CreateMap<DeliveryMethod, DeliveryMethodDto>();
            #endregion

        }

    }
}
