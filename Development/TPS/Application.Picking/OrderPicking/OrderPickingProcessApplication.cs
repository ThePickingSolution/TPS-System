using Application.Picking.Interface.DTOs;
using Application.Picking.Interface.OrderPickings;
using System;

namespace Application.Picking.OrderPicking
{
    public class OrderPickingProcessApplication : IOrderPickingProcessApplication
    {
        public OrderPickingDto NextOrderToPick(string area)
        {
            throw new NotImplementedException();
        }

        public bool Start(OrderPickingDto picking)
        {
            throw new NotImplementedException();
        }
    }
}
