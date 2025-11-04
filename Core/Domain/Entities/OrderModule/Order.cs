using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ProductModule;

namespace Domain.Entities.OrderModule
{
    public class Order:BaseEntity<Guid>
    {
        public Order(string _UserEmail,ShippingAddress _Address,DeliveryMethod _DeliveryMethod, ICollection<OrderItem> _Items, decimal _Subtotal)
        {
            UserEmail = _UserEmail;
            Address = _Address;
            DeliveryMethod = _DeliveryMethod;
            Items = _Items;
            Subtotal = _Subtotal;
        }



        public string UserEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; }
        public ShippingAddress Address { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        public int DeliveryMethodId { get; set; }  //FK
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal Subtotal { get; set; }
        public decimal GetTotal() =>  Subtotal + DeliveryMethod.Price;
        
    }
}
