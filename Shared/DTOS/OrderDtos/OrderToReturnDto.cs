using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDtos
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; }
        public ShippingAddressDto Address { get; set; } = null!;
        public string DeliveryMethod { get; set; } = null!;
        public string OrderStatus { get; set; } = null!;
        public ICollection<OrderItemsDto> Items{ get; set; } = [];
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
