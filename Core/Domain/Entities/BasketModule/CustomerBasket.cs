using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; }   //GUID : Created from Client [Frontend]
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
