using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Picking.Entities
{
    public class OrderPickingEntity
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<OrderPickingDetailEntity> Details { get; set; }
        public ICollection<PickingItemEntity> Items { get; set; }
        public ICollection<OrderPickingProcessEntity> Processes { get; set; }
    }
}
