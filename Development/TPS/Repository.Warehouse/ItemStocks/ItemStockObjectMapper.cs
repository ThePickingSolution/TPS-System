using Business.Domain.Warehouse.Stock;
using Database.Warehouse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Warehouse.ItemStocks
{
    internal static class ItemStockObjectMapper
    {
        public static ItemStock ToDomain(this ItemStockEntity entity) {
            if (entity == null)
                return null;


            return new ItemStock() {
                StockCode = entity.StockCode,
                SKU = entity.SKU,
                Details = entity.Details,
                Priority = 1,
                Sector = null
            };
        } 
    }
}
