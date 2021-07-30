using Business.Domain.Picking;
using Database.Picking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Picking.OrderPickings
{
    public static class OrderPickingObjectMapper
    {
        public static OrderPicking ToDomain(this OrderPickingEntity entity) {
            if (entity == null)
                return null;

            var lastStatus = entity.Processes.OrderBy(o => o.Date).LastOrDefault();

            var model = new OrderPicking(entity.Id, lastStatus.Container, lastStatus.Sector, (PickingStatus)lastStatus.Status_Id, null)
            {
                Description = entity.Description,
                WithContainer = false
            };

            entity.Details.ToList().ForEach(detail => model.Details.Add(detail.Name, detail.Value));
            return model;
        }
    }
}
