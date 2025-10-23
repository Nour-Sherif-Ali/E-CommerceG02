using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Shared;
using Shared.Enums;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product,int>
    {
        //Get All
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams)
            : base(P=>(!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId) &&(!queryParams.TypeId.HasValue ||P.TypeId== queryParams.TypeId)
            &&(string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.Contains(queryParams.SearchValue.ToLower())))
            
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            #region Sorting
            switch (queryParams.SortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    break;
            }
            #endregion

            ApplyPagination(queryParams.PageIndex, queryParams.PageSize);
        }
        //GetById
        public ProductWithBrandAndTypeSpecifications(int Id) :base(P => P.Id == Id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

        }
    }
}
