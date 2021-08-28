using Business.Domain.Events;
using Business.Domain.Picking;
using Repository.Picking.Interface.OrderPickings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.TPSCommon.Picking.Business
{
    public class OrderPickingEvent : IOrderPickingEvent
    {

        private readonly IOrderPickingUpdateRepository updateRepository;

        public OrderPickingEvent(IOrderPickingUpdateRepository _orderPickingUpdateRepository) {
            updateRepository = _orderPickingUpdateRepository;
        }

        public void SetThisEventTo(OrderPicking model) {
            model.Event = this;
        }

        public void OnContainerChange(OrderPicking picking) {
            //Must save in the database quehn container changes
            //It will be saved by setting the status again
            picking.Status = picking.Status;
        }

        public void OnStatusChange(OrderPicking picking, PickingStatus previousStatus) {
            updateRepository.UpdateStatus(picking);
        }
    }
}
