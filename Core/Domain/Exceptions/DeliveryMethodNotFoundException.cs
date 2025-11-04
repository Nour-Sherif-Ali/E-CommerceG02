using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class DeliveryMethodNotFoundException(int id) : NotFoundException($"No Delivery Method Was Found For This ID: {id} Numebr")
    {
    }
}
