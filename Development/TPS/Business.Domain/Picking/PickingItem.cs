using Business.Domain.Exceptions;
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
        private ItemStatus _status;
        private IPickingItemValidator _validator = new DefaultPickingItemValidator();

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
            }
        }
        public ItemStatus Status {
            get => _status;
            set
            {
                _validator.ValidateStatusChange(_status, value, this);
                _status = value;
            }
        }
        public IDictionary<string, string> Details { get; } = new Dictionary<string, string>();

        public IPickingItemValidator Validator
        {
            set
            {
                if (value == null)
                    throw new NullValidatorException("Picking Item Validator");
                _validator = value;
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
    
    

    }
}
