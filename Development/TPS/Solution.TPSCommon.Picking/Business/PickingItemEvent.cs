using Business.Domain.Events;
using Business.Domain.Picking;
using Business.Domain.Validations;
using Picking.Hardware.Handler.Interface.Message;
using Repository.Picking.Interface.OrderPickings;
using Repository.Picking.Interface.PickingItems;
using Service.PickToLight.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.TPSCommon.Picking.Business
{
    public class PickingItemEvent : IPickingItemEvent
    {
        private readonly IPickingFaceService pickingFace;
        private readonly IOrderPickingQuery orderPickingQuery;
        private readonly IPickingItemUpdateRepository itemUpdate;

        private readonly IOrderPickingEvent opEvent;
        private readonly IOrderPickingValidator opValidator;

        public PickingItemEvent(IPickingFaceService _pickingFace
            , IOrderPickingQuery _orderPickingQuery
            , IPickingItemUpdateRepository _itemUpdate
            , IOrderPickingEvent _opEvent
            , IOrderPickingValidator _opValidator) {

            pickingFace = _pickingFace;
            orderPickingQuery = _orderPickingQuery;
            itemUpdate = _itemUpdate;

            opEvent = _opEvent;
            opValidator = _opValidator;
        }

        public void SetThisEventTo(PickingItem model) {
            model.Event = this;
        }

        public void OnBarcodeChange(PickingItem item) {
            //No actions
        }

        public void OnStatusChange(PickingItem item, ItemStatus previousStatus) {
            itemUpdate.UpdateStatus(item);
            PickToLightProcess(item);
        }


        private void PickToLightProcess(PickingItem item) {
            var orderPicking = orderPickingQuery.New()
                .ContainsItem(item.Id)
                .FirstOrDefault();

            orderPicking.Validator = this.opValidator;
            orderPicking.Event = this.opEvent;

            var pendindActionStatuses = new ItemStatus[]{
                ItemStatus.NO_READING,
                ItemStatus.PENDING,
                ItemStatus.PENDING_READING
            };

            //TODO Save Sector in item processo too.
            if (!orderPicking.Items.Any(i => pendindActionStatuses.Contains(i.Status))) {
                orderPicking.Status = PickingStatus.PICKED;
                this.pickingFace.Finish(item.SKU, orderPicking);
            }
        }

    }
}
