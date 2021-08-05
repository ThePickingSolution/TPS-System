using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Warehouse.Stock
{
    public class Sector
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<ItemStock> Stocks { get; set; }
    }
}
