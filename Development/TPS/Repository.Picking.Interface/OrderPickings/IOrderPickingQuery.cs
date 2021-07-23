using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Picking.Interface.OrderPickings
{
    public interface IOrderPickingQuery
    {
        OrderPicking FirstOrDefault();
        List<OrderPicking> Get();

        IOrderPickingQuery FilterById(string id);
        IOrderPickingQuery FilterByUser(string username);
        IOrderPickingQuery FilterByStatus(PickingStatus status);
        IOrderPickingQuery FilterByContainer(string container);
        IOrderPickingQuery FilterByArea(string area);

        IOrderPickingQuery New();
    }
}
