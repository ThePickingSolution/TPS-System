using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picking.Hardware.Handler.Interface.Message
{
    public interface IPickingFacePostman
    {
        void PickManyRef(OrderPicking picking);
        void PickOneRef(List<PickingItem> items, string sector);
        void FinishPicking(PickingItem lastItem, string sector);
    }
}