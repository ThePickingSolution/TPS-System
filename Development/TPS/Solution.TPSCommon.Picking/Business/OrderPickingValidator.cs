using Business.Domain.Exceptions;
using Business.Domain.Picking;
using Business.Domain.Validations;
using Infrastructure.String;

namespace Solution.TPSCommon.Picking.Business
{
    public class OrderPickingValidator : IOrderPickingValidator
    {
        public void SetThisValidatorTo(OrderPicking model) {
            model.Validator = this;
        }

        public void ValidateContainer(string code, OrderPicking picking) {
            if (picking.Status != PickingStatus.PENDING)
                throw new InvalidContainerException("Não é possivel alterar o recipiente para este order picking");
            if(code.IsNullOrEmpty())
                throw new InvalidContainerException("Código do recipiente é nulo");
        }

        public void ValidateStatusChange(PickingStatus current, PickingStatus to, OrderPicking picking) {
            string error = string.Empty;
            switch (current) {
                case PickingStatus.PICKED:
                error = "Order picking já finalizado";
                break;
                case PickingStatus.READY:
                if (to == PickingStatus.PENDING)
                    error = "Mudança de estado não permitida";
                break;
            }
            if (!error.IsNullOrEmpty())
                throw new InvalidStatusChangeException(error);
        }
    }
}
