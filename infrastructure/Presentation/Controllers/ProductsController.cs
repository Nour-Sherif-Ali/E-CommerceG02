using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.DTOS;
namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]  // BaseUrl / api / ProductsController
    public class ProductsController(IServiceManager _serviceManager ) : ControllerBase
    {
        #region Get All Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(Products);
        }

        #endregion

        #region Get Product By Id
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
