using Application.Picking.Interface.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Picking.Interface.OrderPickings
{
    public interface IOrderPickingProcessApplication
    {
        OrderPickingDto NextOrderToPick(string area);

        /// <summary>
        /// Returns true when picking is assigned to the operator; and false when order picking is assigned to another operator already
        /// </summary>
        /// <param name="picking"></param>
        /// <returns></returns>
        bool Start(OrderPickingDto picking);
    }
}
