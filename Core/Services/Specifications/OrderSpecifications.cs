using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderModule;

namespace Services.Specifications
{
    public class OrderSpecifications : BaseSpecifications<Order,Guid>
    {
        //Get ALL orders By Email
        public OrderSpecifications(string Email):base(o =>o.UserEmail ==Email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
            AddOrderByDescending(o => o.OrderDate);
        }
        //Get Order By Id
        public OrderSpecifications(Guid Id) : base(o => o.Id == Id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }
    }
}
