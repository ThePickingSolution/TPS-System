using Application.Picking.Interface.DTOs;
using Application.Picking.Interface.DTOs.Extensions;
using Application.Picking.Interface.DTOs.Params;
using Application.Picking.Interface.OrderPickings;
using Repository.Picking.Interface.OrderPickings;
using System.Collections.Generic;

namespace Application.Picking.OrderPicking
{
    public class OrderPickingApplication : IOrderPickingApplication
    {

        private readonly IOrderPickingQuery baseQuery;

        public OrderPickingApplication(IOrderPickingQuery _baseQuery) {
            baseQuery = _baseQuery;
        }


        public IEnumerable<OrderPickingDto> Get(OrderPickingParams parameters) {
            if (parameters == null)
                return new List<OrderPickingDto>();

            var op_query = baseQuery.New();

            if (parameters.FilterById)
                op_query.FilterById(parameters.Id);
            if (parameters.FilterBySector)
                op_query.FilterBySector(parameters.Sector);
            if (parameters.FilterByContainer)
                op_query.FilterByContainer(parameters.Container);
            if (parameters.FilterByOperator)
                op_query.FilterByUser(parameters.Operator);
            if (parameters.FilterByStatus)
                op_query.FilterByStatus(parameters.Status);

            return op_query.Get(parameters.Limit).ParseDtos();

        }
    
    }
}
