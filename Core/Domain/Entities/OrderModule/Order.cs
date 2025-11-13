using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ProductModule;

namespace Domain.Entities.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        // EF Core needs this
        public Order() { }

       
        public Order(string userEmail, ShippingAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal)
        {
            UserEmail = userEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
        }

        public string UserEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; }
        public ShippingAddress Address { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        public int DeliveryMethodId { get; set; }
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal Subtotal { get; set; }

        public decimal GetTotal() => Subtotal + DeliveryMethod.Price;
    }

}
