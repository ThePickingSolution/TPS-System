using Business.Domain.Exceptions;
using Business.Domain.Picking;
using Infrastructure.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Validations
{
    public interface IPickingItemValidator
    {
        /// <summary>
        /// Validate picking item barcode 
        /// 
        /// Exceptions:
        ///     InvalidItemBarcodeException
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="item"></param>
        void ValidateItemBarcode(string barcode, PickingItem item);

        /// <summary>
        /// Validate change of Picking Item status
        /// 
        /// Exceptions:
        ///     InvalidStatusChangeException
        /// </summary>
        /// <param name="current"></param>
        /// <param name="changeTo"></param>
        /// <param name="item"></param>
        void ValidateStatusChange(ItemStatus current, ItemStatus changeTo, PickingItem item);
    }

    public class DefaultPickingItemValidator : IPickingItemValidator
    {
        public void ValidateItemBarcode(string barcode, PickingItem item)
        {
            if (barcode.IsNullOrEmpty())
                throw new InvalidItemBarcodeException("Código de barras vazio é invalido");
        }

        public void ValidateStatusChange(ItemStatus current, ItemStatus changeTo, PickingItem item)
        {
            if (current == ItemStatus.PICKED)
                throw new InvalidStatusChangeException("Produto já separado");

        }
    }
}
