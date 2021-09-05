using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Picking.Interface.PickingItems
{
    public interface IPickingItemProcessApplication
    {
        void SetItemStatus(string id, ItemStatus status, string userid);
    }
}
