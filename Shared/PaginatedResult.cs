using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult(int _PageIndex, int _PageSize, int _TotalCount, IEnumerable<TEntity> _Data) 
        {
            PageIndex = _PageIndex;
            PageSize = _PageSize;
            TotalCount = _TotalCount;
            Data = _Data;  

        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TEntity> Data { get; set; }

    }
}
