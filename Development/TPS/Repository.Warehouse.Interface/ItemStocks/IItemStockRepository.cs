using Business.Domain.Warehouse.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Warehouse.Interface.ItemStocks
{
    public interface IItemStockRepository
    {

        IEnumerable<ItemStock> GetItems(string sector);

        string StockCode(string sector, string sku);

    }
}
