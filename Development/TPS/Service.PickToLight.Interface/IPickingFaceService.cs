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

        void Approve(string sku, OrderPicking picking);
        void Reject(string sku, OrderPicking picking);
        void Finish(string sku, OrderPicking picking);

        void Cancel(OrderPicking picking);
        void Error(string sector);
    }
}
