using Business.Domain.Events;
using Business.Domain.Exceptions;
using Business.Domain.Picking;
using Picking.Hardware.Handler.Interface.Message;
using Repository.Picking.Interface.OrderPickings;
using Service.PickToLight.Interface;

namespace Solution.TPSCommon.Picking.Business
{
    public class OrderPickingEvent : IOrderPickingEvent
    {

        private readonly IOrderPickingUpdateRepository updateRepository;
        private readonly IPickingFaceService pickingface;

        public OrderPickingEvent(IOrderPickingUpdateRepository _orderPickingUpdateRepository
            , IPickingFaceService _pickingface
            ) {
            updateRepository = _orderPickingUpdateRepository;
            pickingface = _pickingface;
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
            PickToLightProcess(picking, previousStatus);
        }


        private void PickToLightProcess(OrderPicking picking, PickingStatus previousStatus) {

            if (picking.Status == PickingStatus.WIP && previousStatus != PickingStatus.WIP) {
                if (!this.pickingface.Start(picking)) {
                    picking.Status = PickingStatus.PENDING;
                    this.pickingface.Cancel(picking);
                    throw new DomainException("Não foi possivel comunicar-se com a interface");
                }
            }

         
        }

    }
}
