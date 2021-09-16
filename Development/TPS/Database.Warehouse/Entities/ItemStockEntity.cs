    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Warehouse.Entities
{
    public class ItemStockEntity
    {
        public int Id { get; set; }
        public string StockCode { get; set; }
        public string SKU { get; set; }
        public string Details { get; set; }

        public int Sector_Id { get; set; }
        public SectorEntity Sector { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
