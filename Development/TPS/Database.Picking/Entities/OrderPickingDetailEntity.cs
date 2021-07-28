using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Picking.Entities
{
    public class OrderPickingDetailEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public string OrderPicking_Id { get; set; }
        public OrderPickingEntity OrderPicking { get; set; }
    }
}
