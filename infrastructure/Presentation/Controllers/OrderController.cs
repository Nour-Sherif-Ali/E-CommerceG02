using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DTOS.OrderDtos;

namespace Presentation.Controllers
{
    public class OrderController(IServiceManager _serviceManager) : APIBaseController
    {
        #region Create Order
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            //var Email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await _serviceManager.OrderService.CreateOrder(orderDto, GetEmailFromToken());
            return Ok(Order);
        }
        #endregion
        #region GetDeliveryMethods
        [Authorize]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>>GetDeliveryMethods()
        {
            var DeliveryMethods = await _serviceManager.OrderService.GetAllDeliveryMethods();
            return Ok(DeliveryMethods);
        }
        #endregion
        #region GetAllOrderByEmail
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var Order = await _serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Order);
        }
        #endregion
        #region GetOrderById
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var Order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(Order);
        }

        #endregion

    }
}
