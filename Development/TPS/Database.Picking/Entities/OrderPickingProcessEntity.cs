using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Picking.Entities
{
    public class OrderPickingProcessEntity
    {
        public int Id { get; set; }

        public string OrderPicking_Id { get; set; }
        public OrderPickingEntity OrderPicking { get; set; }

        public int Status_Id { get; set; }
        public OrderPickingStatusEntity Status { get; set; }
        public string Operator { get; set; }
        public string Sector { get; set; }
        public string Container { get; set; }
        public DateTime Date { get; set; }
    }
}
