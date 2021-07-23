using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Events
{
    public interface IPickingItemEvent
    {
        void OnStatusChange(PickingItem item, ItemStatus previousStatus);
        void OnBarcodeChange(PickingItem item);
    }

    public class DefaultPickingItemEvent : IPickingItemEvent
    {
        public void OnBarcodeChange(PickingItem item)
        {
            return;
        }

        public void OnStatusChange(PickingItem item, ItemStatus previousStatus)
        {
            return;
        }
    }
}
