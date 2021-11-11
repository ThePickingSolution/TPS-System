using Application.Picking.Interface.DTOs;
using Application.Picking.Interface.OrderPickings;
using Business.Domain.Events;
using Business.Domain.Exceptions;
using Business.Domain.Picking;
using Business.Domain.Services;
using Business.Domain.Validations;
using Repository.Picking.Interface.Operators;
using Repository.Picking.Interface.OrderPickings;
using System;

namespace Application.Picking.OrderPicking
{
    public class OrderPickingProcessApplication : IOrderPickingProcessApplication
    {
        private readonly INextOrderPickingService nextPickingService;
        private readonly IOrderPickingQuery orderPickingQuery;
        private readonly IOperatorRepository operatorRepository;

        private IOrderPickingEvent orderpickingEvents;
        private IOrderPickingValidator orderpickingValidators;

        public OrderPickingProcessApplication(IOrderPickingQuery _orderPickingQuery
            , IOperatorRepository _operatorRepository
            , INextOrderPickingService _nextPickingService
            , IOrderPickingEvent _orderpickingEvents
            , IOrderPickingValidator _orderpickingValidators) {
            orderPickingQuery = _orderPickingQuery;
            operatorRepository = _operatorRepository;
            nextPickingService = _nextPickingService;
            orderpickingEvents = _orderpickingEvents;
            orderpickingValidators = _orderpickingValidators;
        }

        public string Next(string sector) {
            var orderPicking = nextPickingService.NextOrderPicking(sector);
            return orderPicking == null ? null : orderPicking.Id;
        }


        public bool Start(string id, string sector, string userid)
        {
            var query = orderPickingQuery.New();
            var orderPicking = query
                .FilterById(id)
                .FirstOrDefault();

            var query_pcikign_operator = orderPickingQuery.New();
            var orderPickingOfOperator = query_pcikign_operator
                .FilterByUser(userid)
                //.FilterBySector(sector)
                .FilterByStatus(PickingStatus.WIP)
                .FirstOrDefault();

            if(orderPickingOfOperator != null) {
                throw new DomainException("Usuario não finalizou o picking ainda");
            }

            if (orderPicking == null)
                throw new OrderPickingNotFoundException("Order Picking não encontrada");

            if (orderPicking.Status != PickingStatus.PENDING && orderPicking.Status != PickingStatus.READY)
                return false;

            orderPicking.Event = orderpickingEvents;
            orderPicking.Validator = orderpickingValidators;

            orderPicking.SetPickingOrderToUser(operatorRepository.Get(userid), sector);
            return true;
        }
    }
}
