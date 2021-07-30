using Business.Domain.Picking;
using Database.Picking;
using Database.Picking.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Picking.Interface.OrderPickings;
using System.Collections.Generic;
using System.Linq;

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
                .Include(i => i.Processes)
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


        public IOrderPickingQuery FilterBySector(string sector)
        {
            this.query = this.query.Where(
                    q => q.Processes.OrderByDescending(o => o.Date).First().Sector == sector);
            return this;
        }

        public IOrderPickingQuery FilterByContainer(string container)
        {
            this.query = this.query.Where(
                q => q.Processes.OrderByDescending(o => o.Date).First().Container == container);
            return this;
        }

        public IOrderPickingQuery FilterById(string id)
        {
            this.query = this.query.Where(q => q.Id == id);
            return this;
        }

        public IOrderPickingQuery FilterByStatus(PickingStatus status)
        {
            this.query = this.query.Where(
                    q => q.Processes.OrderByDescending(o => o.Date).First().Status_Id == (int)status);
            return this;
        }

        public IOrderPickingQuery FilterByUser(string username)
        {
            this.query = this.query.Where(
                   q => q.Processes.OrderByDescending(o => o.Date).First().Operator == username);
            return this;
        }

        
    }
}
