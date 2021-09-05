using Business.Domain.Picking;
using System.Collections.Generic;

namespace Repository.Picking.Interface.PickingItems
{
    public interface IPickingItemQuery
    {
        PickingItem FirstOrDefault();
        List<PickingItem> Get(int? limit);

        IPickingItemQuery FilterById(string id);

        IPickingItemQuery New();
    }
}
