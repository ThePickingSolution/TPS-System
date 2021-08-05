using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Warehouse.Entities
{
    public class SectorEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<ItemStockEntity> Stocks { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
