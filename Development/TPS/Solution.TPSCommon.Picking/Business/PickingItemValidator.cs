using Business.Domain.Exceptions;
using Business.Domain.Picking;
using Business.Domain.Validations;
using Infrastructure.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.TPSCommon.Picking.Business
{
    public class PickingItemValidator : IPickingItemValidator
    {
        public PickingItemValidator() {}

        public void SetThisValidatorTo(PickingItem model) {
            model.Validator = this;
        }

        public void ValidateItemBarcode(string barcode, PickingItem item) {
            if (barcode.IsNullOrEmpty())
                throw new InvalidItemBarcodeException("Código do produto está vazio");
        }

        public void ValidateStatusChange(ItemStatus current, ItemStatus changeTo, PickingItem item) {
            string error = string.Empty;
            switch (current) {
                case ItemStatus.PICKED:
                error = "Leitura do item já realizada";
                break;
            }
            if (!error.IsNullOrEmpty())
                throw new InvalidStatusChangeException(error);
        }
    }
}
