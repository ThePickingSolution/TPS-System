using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Picking.Entities
{
    public class PickingItemProcessEntity
    {
        public int Id { get; set; }

        public string PickingItem_Id { get; set; }
        public PickingItemEntity PickingItem { get; set; }

        public int Status_Id { get; set; }
        public PickingItemStatusEntity Status { get; set; }

        public string Operator { get; set; }
        public string Barcode { get; set; }
        public DateTime Date { get; set; }
    }
}
