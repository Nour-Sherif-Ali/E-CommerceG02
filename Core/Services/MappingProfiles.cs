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
            CreateMap<ShippingAddressDto, ShippingAddress>();
            #endregion
        }
        
    }
}
