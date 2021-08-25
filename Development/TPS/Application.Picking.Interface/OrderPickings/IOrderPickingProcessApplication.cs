using Application.Picking.Interface.DTOs;
using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Picking.Interface.OrderPickings
{
    public interface IOrderPickingProcessApplication
    {
        /// <summary>
        /// Returns true when picking is assigned to the operator; and false when order picking is assigned to another operator already
        /// </summary>
        /// <param name="picking"></param>
        /// <returns></returns>
        bool Start(string id, string sector, string userid);

        /// <summary>
        /// Get next pending order picking
        /// </summary>
        /// <param name="sector"></param>
        /// <returns></returns>
        string Next(string sector);
    }
}
