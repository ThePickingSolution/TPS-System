using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Picking.Interface.DTOs
{
    public class PickingItemDto
    {
        public string Id { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public ItemStatus Status { get; set; }
        public List<PickingItemDetailDto> Details { get; set; }
        public string Operator { get; set; }
    }
}
