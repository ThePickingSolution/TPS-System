using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Picking.Interface.DTOs
{
    public class OrderPickingDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public bool ScanContainer { get; set; }
        public string Container { get; set; }
        public string Sector { get; set; }
        public string Operator { get; set; }
        public PickingStatus Status { get; set; }
        public List<OrderPickingDetailDto> Details { get; set; }
    }
}
