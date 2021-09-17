using Business.Domain.Warehouse.Stock;
using Database.Warehouse;
using Repository.Warehouse.Interface.ItemStocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Warehouse.ItemStocks
{
    public class ItemStockRepository : IItemStockRepository
    {
        private readonly WarehouseDbContext _dbContext;
        public ItemStockRepository(WarehouseDbContext dbContext) {
            _dbContext = dbContext;
        }
        public IEnumerable<ItemStock> GetItems(string sector) {
            return _dbContext.ItemStocks
                .Where(s => !s.IsDeleted && s.Sector.Code == sector)
                .ToList()
                .Select(s => s.ToDomain());
        }

        public string StockCode(string sector, string sku) {
            return _dbContext.ItemStocks
                .Where(s => !s.IsDeleted && s.Sector.Code == sector && s.SKU == sku)
                .Select(s => s.StockCode)
                .FirstOrDefault();
        }
    }
}
