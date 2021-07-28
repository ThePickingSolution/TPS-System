using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Picking.Entities
{
    public class PickingItemEntity
    {
        public string Id { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public ICollection<PickingItemDetailEntity> Details { get; set; }

        public OrderPickingEntity OrderPicking { get; set; }
        public string OrderPicking_Id { get; set; }
    }
}
