using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Events
{
    public interface IOrderPickingEvent : IPickingEvent<OrderPicking>
    {
        void OnStatusChange(OrderPicking picking, PickingStatus previousStatus);
        void OnContainerChange(OrderPicking picking);
    }
    public class DefaultOrderPickingEvent : IOrderPickingEvent
    {
        public void OnContainerChange(OrderPicking picking)
        {
            return;
        }

        public void OnStatusChange(OrderPicking picking, PickingStatus previousStatus)
        {
            return;
        }

        public void SetThisEventTo(OrderPicking orderPicking) {
            orderPicking.Event = this;
        }
    }
}
