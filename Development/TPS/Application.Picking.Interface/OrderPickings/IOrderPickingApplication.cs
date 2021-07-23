using Application.Picking.Interface.DTOs;
using Application.Picking.Interface.DTOs.Params;
using System.Collections.Generic;

namespace Application.Picking.Interface.OrderPickings
{
    public interface IOrderPickingApplication
    {
        IEnumerable<OrderPickingDto> Get(OrderPickingParams parameters);
    }
}
