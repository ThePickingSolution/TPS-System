using Business.Domain.Exceptions;
using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Validations
{
    public interface IOrderPickingValidator
    {
        /// <summary>
        /// Validate container code.
        /// 
        /// Exception:
        ///     InvalidContainerException: Throw when code is not valid
        /// </summary>
        /// <param name="container">Container code</param>
        void ValidateContainer(string code, OrderPicking picking);
        /// <summary>
        /// Validate status change of the order picking
        /// 
        /// Exception:
        ///     InvalidStatusChangeException: Throw when change is invalid
        /// </summary>
        /// <param name="current">Current status</param>
        /// <param name="to">Change to status</param>
        /// <param name="picking">Of which order picking</param>
        void ValidateStatusChange(PickingStatus current, PickingStatus to, OrderPicking picking);
    }

    public class DefaultOrderPickingValidator : IOrderPickingValidator
    {
        public void ValidateContainer(string code, OrderPicking picking)
        {
            if (string.IsNullOrEmpty(code))
                throw new InvalidContainerException("Código do recipiente vazio");
        }

        public void ValidateStatusChange(PickingStatus current, PickingStatus to, OrderPicking picking)
        {
            string message = string.Empty;
            if (current == PickingStatus.READY && to == PickingStatus.PENDING)
                message = "Estado da order picking não pode retornar para pendente após ser realizada parcialmente";
            if (current == PickingStatus.PICKED)
                message = "Order picking já foi finalizada";
            throw new InvalidStatusChangeException(message);
        }
    }
}
