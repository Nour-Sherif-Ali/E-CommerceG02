using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ProductModule;

namespace Domain.Entities.OrderModule
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public string ShortName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string DeliveryTime { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
