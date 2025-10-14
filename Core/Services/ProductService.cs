using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Shared.DTOS;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetReposityory<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return BrandsDto;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
           var Products = await _unitOfWork.GetReposityory<Product, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetReposityory<ProductType,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>,IEnumerable < TypeDto >> (Types);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var Product = await _unitOfWork.GetReposityory<Product,int>().GetByIdAsync(id);
            return _mapper.Map<Product, ProductDto>(Product);

        }
    }
}
