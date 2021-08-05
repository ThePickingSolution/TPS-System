using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Warehouse.Stock
{
    public class ItemStock
    {
        public string StockCode { get; set; }
        public string SKU { get; set; }
        public int Priority { get; set; }

        public string Details { get; set; }
    }
}
