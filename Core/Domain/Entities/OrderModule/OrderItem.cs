using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ProductModule;

namespace Domain.Entities.OrderModule
{
    public class OrderItem:BaseEntity<int>
    {
        public ProductItemOrdered Product { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
