using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DTOS.BasketDtos;

namespace Presentation.Controllers
{
    
    
    public class BasketsController(IServiceManager _serviceManager) : APIBaseController
    {
        //Get Basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(Basket);
        }


        //Create Or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var updatedBasket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }

        //Delete Basket
        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var result = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(result);
        }
    }
}
