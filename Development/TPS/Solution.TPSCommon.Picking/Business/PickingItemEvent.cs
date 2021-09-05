using Business.Domain.Events;
using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.TPSCommon.Picking.Business
{
    public class PickingItemEvent : IPickingItemEvent
    {
        //private readonly IOrderPickingUpdateRepository updateRepository;

        public PickingItemEvent() {}

        public void SetThisEventTo(PickingItem model) {
            model.Event = this;
        }

        public void OnBarcodeChange(PickingItem item) {
            //No actions
        }

        public void OnStatusChange(PickingItem item, ItemStatus previousStatus) {
            
        }

    }
}
