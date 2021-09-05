using Business.Domain.Picking;
using Database.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Picking.Interface.OrderPickings
{
    public interface IOrderPickingUpdateRepository
    {
        void UpdateStatus(OrderPicking picking);
    }
}
