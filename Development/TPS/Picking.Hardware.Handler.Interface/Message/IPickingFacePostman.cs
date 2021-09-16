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
        void PickManyRef(OrderPicking picking, string itemStockCode);
        void PickOneRef(List<PickingItem> items, string itemStockCode);
        void FinishPicking(PickingItem lastItem, string itemStockCode);
    }
}