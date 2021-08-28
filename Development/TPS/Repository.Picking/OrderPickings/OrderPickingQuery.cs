using Business.Domain.People;
using Business.Domain.Picking;
using Database.Picking;
using Database.Picking.Entities;
using Infrastructure.String;
using Microsoft.EntityFrameworkCore;
using Repository.Picking.Interface.Operators;
using Repository.Picking.Interface.OrderPickings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Picking.OrderPickings
{
    public class OrderPickingQuery : IOrderPickingQuery
    {
        private readonly PickingDbContext _dbContext;
        private readonly IOperatorRepository _operatorRepository;

        private IQueryable<OrderPickingEntity> query;

        public OrderPickingQuery(PickingDbContext dbContext,
            IOperatorRepository operatorRepository) {
            _dbContext = dbContext;
            _operatorRepository = operatorRepository;

            query = dbContext.OrderPickings
                .Include(i => i.Items).ThenInclude(i => i.Details)
                .Include(i => i.Items).ThenInclude(i => i.Processes)
                .Include(i => i.Processes)
                .Include(i => i.Details);
        }

        public OrderPicking FirstOrDefault() {
            var orderPicking = this.query.FirstOrDefault();
            if (orderPicking == null)
                return null;
            return Convert(orderPicking);
        }

        public List<OrderPicking> Get(int? limit) {
            var orders = limit.HasValue ? this.query.Take(limit.Value).ToList() : this.query.ToList();
            return orders.Select(entity => Convert(entity)).ToList();
        }

        private OrderPicking Convert(OrderPickingEntity orderPicking) {
            string opid = orderPicking.Processes.OrderBy(o => o.Date).Last().Operator;
            Operator op = null;
            if (!opid.IsNullOrEmpty())
                op = this._operatorRepository.Get(opid);
            return orderPicking.ToDomain(op);
        }

        public IOrderPickingQuery New() {
            return new OrderPickingQuery(_dbContext, _operatorRepository);
        }


        public IOrderPickingQuery FilterBySector(string sector) {
            this.query = this.query.Where(
                    q => q.Processes.OrderByDescending(o => o.Date).First().Sector == sector);
            return this;
        }

        public IOrderPickingQuery FilterByContainer(string container) {
            this.query = this.query.Where(
                q => q.Processes.OrderByDescending(o => o.Date).First().Container == container);
            return this;
        }

        public IOrderPickingQuery FilterById(string id) {
            this.query = this.query.Where(q => q.Id == id);
            return this;
        }

        public IOrderPickingQuery FilterByStatus(PickingStatus status) {
            this.query = this.query.Where(
                    q => q.Processes.OrderByDescending(o => o.Date).First().Status_Id == (int)status);
            return this;
        }
        public IOrderPickingQuery FilterByOrStatus(List<PickingStatus> anyStatus) {
            var statusIds = anyStatus.Cast<int>();
            this.query = this.query.Where(
                    q => statusIds.Contains(
                        q.Processes.OrderByDescending(o => o.Date).First().Status_Id)
                    );
            return this;
        }

        public IOrderPickingQuery FilterByUser(string username) {
            this.query = this.query.Where(
                   q => q.Processes.OrderByDescending(o => o.Date).First().Operator == username);
            return this;
        }

        public IOrderPickingQuery FilterByDetail(string key, string value) {
            this.query = this.query.Where(
                   q => q.Details.Any(d => d.Name == key && d.Value == value));
            return this;
        }

        public IOrderPickingQuery FilterByDetail(string key, Expression<Func<string, bool>> condition) {
            var filterFunc = condition.Compile();
            this.query = this.query.Where(
                   q => q.Details.Any(d => d.Name == key && filterFunc.Invoke(d.Value)));
            return this;
        }

        public IOrderPickingQuery OrderByDate(bool asc) {
            this.query = asc ?
                this.query.OrderBy(o => o.CreationDate)
                : this.query.OrderByDescending(o => o.CreationDate);

            return this;
        }
    }
}
