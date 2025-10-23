using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ProductNotFoundException(int Id) : NotFoundException($"The Product With Id = {Id} IS not Found")
    {

    }
}
