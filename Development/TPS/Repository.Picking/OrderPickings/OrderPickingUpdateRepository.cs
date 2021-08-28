using Business.Domain.Picking;
using Database.Picking;
using Database.Picking.Entities;
using Repository.Picking.Interface.OrderPickings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Picking.OrderPickings
{
    public class OrderPickingUpdateRepository : IOrderPickingUpdateRepository
    {
        private readonly PickingDbContext _dbContext;

        public OrderPickingUpdateRepository(PickingDbContext dbContext) {
            _dbContext = dbContext;
        }

        public void UpdateStatus(OrderPicking picking) {
            var process = new OrderPickingProcessEntity() {
                Id = 0,
                Container = picking.Container,
                Date = DateTime.Now,
                Operator = picking.Operator == null ? null : picking.Operator.Id.ToString(),
                OrderPicking_Id = picking.Id,
                Sector = picking.Sector,
                Status_Id = (int)picking.Status
            };

            _dbContext.Add(process);
            _dbContext.SaveChanges();
        }
    }
}
