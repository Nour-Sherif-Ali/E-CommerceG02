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
        private const int DefaultPageSize = 5; // han7ot by default 5 products 
        private const int MaxPageSize = 10; //akhro 10 products
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions SortingOptions { get; set; }
        public string? SearchValue { get; set; }
        public int PageIndex { get; set; } = 1; //بعرض في صفحه الاولي
        private int pagesize = DefaultPageSize;
        public int PageSize
        {
            get { return pagesize;  }
            set { pagesize = value > MaxPageSize ? MaxPageSize : value; }
        }
    }
}
