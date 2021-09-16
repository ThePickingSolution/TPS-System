using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Picking.Interface.OrderPickings
{
    public interface IOrderPickingQuery
    {
        OrderPicking FirstOrDefault();
        List<OrderPicking> Get(int? limit);

        IOrderPickingQuery FilterById(string id);
        IOrderPickingQuery FilterByUser(string username);
        IOrderPickingQuery FilterByContainer(string container);
        IOrderPickingQuery FilterBySector(string sector);

        IOrderPickingQuery FilterByStatus(PickingStatus status);
        IOrderPickingQuery FilterByOrStatus(List<PickingStatus> anyStatus);

        IOrderPickingQuery FilterByDetail(string key, string value);
        IOrderPickingQuery FilterByDetail(string key, Expression<Func<string,bool>> condition);

        IOrderPickingQuery ContainsItem(string id);

        IOrderPickingQuery OrderByDate(bool asc);

        IOrderPickingQuery New();
    }
}
