using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Picking.Entities
{
    public class PickingItemDetailEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public string PickingItem_Id { get; set; }
        public PickingItemEntity PickingItem { get; set; }
    }
}
