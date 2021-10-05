using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PickToLight.Interface
{
    public interface IPickingFaceService
    {
        bool Start(OrderPicking picking);

        bool Approve(string sku, OrderPicking picking);
        bool Reject(string sku, OrderPicking picking);
        bool Finish(string sku, OrderPicking picking);

        bool Cancel(OrderPicking picking);
        bool Error(string sector,string reason);
    }
}
