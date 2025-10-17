using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Enums;

namespace Shared
{
    public class ProductQueryParams
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions SortingOptions { get; set; }
        public string? SearchValue { get; set; }
    }
}
