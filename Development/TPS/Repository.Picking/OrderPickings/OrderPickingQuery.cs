using Business.Domain.Picking;
using Database.Picking;
using Database.Picking.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Picking.Interface.OrderPickings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Picking.OrderPickings
{
    public class OrderPickingQuery : IOrderPickingQuery
    {
        private readonly PickingDbContext _dbContext;

        private IQueryable<OrderPickingEntity> query;

        public OrderPickingQuery(PickingDbContext dbContext)
        {
            _dbContext = dbContext;
            query = dbContext.OrderPickings
                .Include(i => i.Items)
                .Include(i => i.Details);
        }

        public OrderPicking FirstOrDefault()
        {
            return this.query.FirstOrDefault().ToDomain();
        }

        public List<OrderPicking> Get()
        {
            return this.query.Select(entity => entity.ToDomain()).ToList();
        }

        public IOrderPickingQuery New()
        {
            return new OrderPickingQuery(_dbContext);
        }


        public IOrderPickingQuery FilterByArea(string area)
        {
            throw new NotImplementedException();
        }

        public IOrderPickingQuery FilterByContainer(string container)
        {
            throw new NotImplementedException();
        }

        public IOrderPickingQuery FilterById(string id)
        {
            this.query = this.query.Where(q => q.Id == id);
            return this;
        }

        public IOrderPickingQuery FilterByStatus(PickingStatus status)
        {
            throw new NotImplementedException();
        }

        public IOrderPickingQuery FilterByUser(string username)
        {
            throw new NotImplementedException();
        }

        
    }
}
