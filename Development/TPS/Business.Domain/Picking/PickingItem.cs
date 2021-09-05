using Business.Domain.Events;
using Business.Domain.Exceptions;
using Business.Domain.People;
using Business.Domain.Validations;
using Infrastructure.String;
using System.Collections.Generic;

namespace Business.Domain.Picking
{
    public class PickingItem
    {
        private string _id;
        private string _SKU;
        private string _barcode;
        private Operator _operator;
        private ItemStatus _status;
        private IPickingItemValidator _validator = new DefaultPickingItemValidator();
        private IPickingItemEvent _event = new DefaultPickingItemEvent();

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
        public string SKU { 
            get => _SKU;
            set {
                if (value.IsNullOrEmpty())
                    throw new InvalidSKUException("SKU do produto não pode ser vazio");
                this._SKU = value;
            }
        }
        public string Description { get; set; }
        public string Barcode {
            get => _barcode;
            set
            {
                _validator.ValidateItemBarcode(value, this);
                _barcode = value;
                _event.OnBarcodeChange(this);
            }
        }
        public ItemStatus Status {
            get => _status;
            set
            {
                _validator.ValidateStatusChange(_status, value, this);
                ItemStatus prevAux = _status;
                _status = value;
                _event.OnStatusChange(this, prevAux);
            }
        }
        public Operator Operator {
            get => _operator;
            set {
                if (value == null)
                    throw new NullOperatorException("Operador deve ser informado");
                _operator = value;
            }
        }
        public IDictionary<string, string> Details { get; } = new Dictionary<string, string>();

        public IPickingItemValidator Validator
        {
            set
            {
                if (value == null)
                    throw new NullValidatorException("Null picking Item Validator");
                _validator = value;
            }
        }
        public IPickingItemEvent Event
        {
            set
            {
                if (value == null)
                    throw new NullValidatorException("Null picking Item Events");
                _event = value;
            }
        }


        public PickingItem(string id) {
            this.Id = id;
        }
        public PickingItem(string id, IPickingItemValidator validator)
        {
            this.Id = id;
            _validator = validator;
        }
        public PickingItem(string id, IPickingItemValidator validator, IPickingItemEvent events)
        {
            this.Id = id;
            _validator = validator;
            _event = events;
        }
        public PickingItem(string id, string sku, string barcode, ItemStatus status, Operator operatorUser)
        {
            _id = id;
            _SKU = sku;
            _barcode = barcode;
            _status = status;
            _operator = operatorUser;
        }

    }
}
