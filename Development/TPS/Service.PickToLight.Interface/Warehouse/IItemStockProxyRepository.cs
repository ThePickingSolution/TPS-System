using Business.Domain.Warehouse.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PickToLight.Interface.Warehouse
{
    public interface IItemStockProxyRepository
    {
        List<ItemStock> Get(string sector);
    }
}
