using Business.Domain.Picking;
using Database.Picking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Picking.PickingItems
{
    internal static class PickingItemObjectMapper
    {
        public static PickingItem ToDomain(this PickingItemEntity entity) {
            if (entity == null)
                return null;

            var lastStatus = entity.Processes.OrderBy(o => o.Date).LastOrDefault();

            var model = new PickingItem(entity.Id, entity.SKU, lastStatus.Barcode, (ItemStatus)lastStatus.Status_Id,null) {
                Description = entity.Description
            };

            entity.Details.ToList().ForEach(detail => model.Details.Add(detail.Name, detail.Value));
            return model;
        }
    }
}
