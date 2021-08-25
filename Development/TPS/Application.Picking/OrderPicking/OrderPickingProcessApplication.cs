using Application.Picking.Interface.DTOs;
using Application.Picking.Interface.OrderPickings;
using Business.Domain.Exceptions;
using Business.Domain.Picking;
using Business.Domain.Services;
using Repository.Picking.Interface.Operators;
using Repository.Picking.Interface.OrderPickings;
using System;

namespace Application.Picking.OrderPicking
{
    public class OrderPickingProcessApplication : IOrderPickingProcessApplication
    {
        private INextOrderPickingService nextPickingService;
        private readonly IOrderPickingQuery orderPickingQuery;
        private readonly IOperatorRepository operatorRepository;

        public OrderPickingProcessApplication(IOrderPickingQuery _orderPickingQuery
            , IOperatorRepository _operatorRepository) {
            orderPickingQuery = _orderPickingQuery;
            operatorRepository = _operatorRepository;
        }

        public string Next(string sector) {
            return nextPickingService.NextOrderPicking(sector);
        }


        public bool Start(string id, string sector, string userid)
        {
            var query = orderPickingQuery.New();
            var orderPicking = query
                .FilterById(id)
                .FirstOrDefault();

            if (orderPicking == null)
                throw new OrderPickingNotFoundException("Order Picking não encontrada");

            if (orderPicking.Status != PickingStatus.PENDING && orderPicking.Status != PickingStatus.READY)
                return false;

            orderPicking.SetPickingOrderToUser(operatorRepository.Get(userid), sector);
            return true;
        }
    }
}
