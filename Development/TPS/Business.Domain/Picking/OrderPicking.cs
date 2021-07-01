using Business.Domain.Exceptions;
using Business.Domain.Validations;
using System;
using System.Collections.Generic;

namespace Business.Domain.Picking
{
    public class OrderPicking
    {
        private string _id;
        public string _container;
        private PickingStatus _status = PickingStatus.PENDING;
        private IOrderPickingValidator _validator = new DefaultOrderPickingValidator();

        public string Id
        {
            get => _id;
            protected set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidOrderPickingIdException("Id vazio");
                _id = value;
            }
        }
        public string Description { get; set; }
        public bool WithContainer { get; set; }
        public string Container
        {
            get => _container;
            set
            {
                _validator.ValidateContainer(value,this);
                _container = value;
            }
        }
        public string User { get; set; }
        public PickingStatus Status
        {
            get => _status;
            set
            {
                _validator.ValidateStatusChange(_status, value, this);
            }
        }
        public IDictionary<string, string> Details { get; } = new Dictionary<string, string>();
        public ICollection<PickingItem> Items { get; protected set; } = new List<PickingItem>();
        
        public IOrderPickingValidator Validator {
            set {
                if (value == null)
                    throw new NullValidatorException("Order Picking Validator");
                _validator = value;
            } 
        }


        public OrderPicking(string id) {
            this.Id = id;
        }
        public OrderPicking(string id,IOrderPickingValidator validator) {
            this.Id = id;
            this._validator = validator;
        }


        public void PutItemsInOrder()
        {
            throw new NotImplementedException();
        }

        //TODO User domain
        
    }
}
