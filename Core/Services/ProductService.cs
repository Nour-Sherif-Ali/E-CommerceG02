using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.DTOS;
using Shared.Enums;

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

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(queryParams);
            var repo = _unitOfWork.GetReposityory<Product, int>();

            // Get filtered + paginated data
            var allProducts = await repo.GetAllAsync(specifications);
            var data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(allProducts);
            var ProductCount = allProducts.Count();

            // Get total count (without pagination)
            var Countspec = new ProductcountSpecifications(queryParams);
            var TotalCount = await repo.CountAsync(Countspec);

            // ✅ Pass parameters in correct order
            return new PaginatedResult<ProductDto>(
                queryParams.PageIndex,
                //queryParams.PageSize,
                ProductCount,
                TotalCount,
                data
            );
        }


        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetReposityory<ProductType,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>,IEnumerable < TypeDto >> (Types);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int Id)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications(Id);
            var Product = await _unitOfWork.GetReposityory<Product,int>().GetByIdAsync(Specifications);
            if(Product is null)
            {
                throw new ProductNotFoundException(Id);
            }
            return _mapper.Map<Product, ProductDto>(Product);

        }
    }
}
