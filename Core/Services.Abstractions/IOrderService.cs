using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.OrderDtos;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string Email);
        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id);
    }
}
