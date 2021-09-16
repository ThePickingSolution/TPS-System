using Application.Picking.Interface.PickingItems;
using Business.Domain.Events;
using Business.Domain.Exceptions;
using Business.Domain.Picking;
using Business.Domain.Validations;
using Repository.Picking.Interface.Operators;
using Repository.Picking.Interface.PickingItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Picking.PickingItems
{
    public class PickingItemProcessApplication : IPickingItemProcessApplication
    {
        private readonly IPickingItemQuery pickingItemQuery;
        private readonly IOperatorRepository operatorRepository;

        private IPickingItemEvent pickingItemEvents;
        private IPickingItemValidator pickingItemValidators;

        public PickingItemProcessApplication(
            IPickingItemQuery _pickingItemQuery
            , IOperatorRepository _operatorRepository
            //, INextOrderPickingService _nextPickingService
            , IPickingItemEvent _pickingItemEvents
            , IPickingItemValidator _pickingItemValidators) {
            pickingItemQuery = _pickingItemQuery;
            operatorRepository = _operatorRepository;
            //nextPickingService = _nextPickingService;
            pickingItemEvents = _pickingItemEvents;
            pickingItemValidators = _pickingItemValidators;
        }

        public void SetItemStatus(string id, ItemStatus status, string userid) {
            var _operator = this.operatorRepository.Get(userid);
            var item = this.pickingItemQuery
                .New()
                .FilterById(id)
                .FirstOrDefault();

            if (item == null)
                throw new PickingNotFoundException("Item não encontrado");

            item.Validator = this.pickingItemValidators;
            item.Event = this.pickingItemEvents;

            item.Operator = _operator;
            item.Status = status;
        }
    }
}
