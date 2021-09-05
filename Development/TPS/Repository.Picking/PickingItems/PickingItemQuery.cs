using Business.Domain.Picking;
using Database.Picking;
using Database.Picking.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Picking.Interface.PickingItems;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Picking.PickingItems
{
    public class PickingItemQuery : IPickingItemQuery
    {
        private readonly PickingDbContext _dbContext;

        private IQueryable<PickingItemEntity> query;

        public PickingItemQuery(PickingDbContext dbContext) {
            _dbContext = dbContext;

            query = dbContext.PickingItems
                .Include(i => i.Processes)
                .Include(i => i.Details);
        }

        public PickingItem FirstOrDefault() {
            var pickingItem = this.query.FirstOrDefault();
            if (pickingItem == null)
                return null;
            return Convert(pickingItem);
        }

        public List<PickingItem> Get(int? limit) {
            var orders = limit.HasValue ? this.query.Take(limit.Value).ToList() : this.query.ToList();
            return orders.Select(entity => Convert(entity)).ToList();
        }

        private PickingItem Convert(PickingItemEntity pickingItem) {
            return pickingItem.ToDomain();
        }

        public IPickingItemQuery New() {
            return new PickingItemQuery(_dbContext);
        }


        public IPickingItemQuery FilterById(string id) {
            this.query = this.query.Where(q => q.Id == id);
            return this;
        }
    }
}
