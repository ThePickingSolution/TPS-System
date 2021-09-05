using Business.Domain.Picking;
using Database.Picking;
using Database.Picking.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Picking.Interface.PickingItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Picking.PickingItems
{
    public class PickingItemUpdateRepository : IPickingItemUpdateRepository
    {
        private readonly PickingDbContext _dbContext;
        public PickingItemUpdateRepository(PickingDbContext dbContext) {
            _dbContext = dbContext;
        }
        public void UpdateStatus(PickingItem item) {

            var op = _dbContext
                .PickingItems
                .Include(i => i.OrderPicking)
                .FirstOrDefault(i => i.Id == item.Id);

            var process = new PickingItemProcessEntity() {
                Id = 0,
                Barcode = item.Barcode,
                Date = DateTime.Now,
                Operator = item.Operator == null ? null : item.Operator.Id.ToString(),
                PickingItem_Id = op.Id,
                Status_Id = (int)item.Status
            };

            _dbContext.PickingItemProcesses.Add(process);
            _dbContext.SaveChanges();
        }
    }
}
