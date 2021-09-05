using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Picking.Interface.PickingItems
{
    public interface IPickingItemUpdateRepository
    {
        void UpdateStatus(PickingItem item);
    }
}
