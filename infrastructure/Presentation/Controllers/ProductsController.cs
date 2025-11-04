using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.DTOS;
using Shared.ErrorModels;
namespace Presentation.Controllers
{


    
    public class ProductsController(IServiceManager _serviceManager ) : APIBaseController
    {
        #region Get All Products
        [Authorize]  
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(Products);
        }

        #endregion

        #region Get Product By Id
        [ProducesResponseType(typeof(ErrorToReturn),(int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorToReturn), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationErrorToReturn ), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]


        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int Id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(Id);
            return Ok(Product);
        }
        #endregion

        #region Get ALl Types
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        #endregion

        #region Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>>GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
        #endregion
    }
}
