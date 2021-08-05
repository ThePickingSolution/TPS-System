using Database.Warehouse.Entities;
using Database.Warehouse.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Warehouse
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) { }

        public DbSet<SectorEntity> Sectors { get; set; }
        public DbSet<ItemStockEntity> ItemStocks { get; set; }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new ItemStockEntityMap());
            modelBuilder.ApplyConfiguration(new SectorEntityMap());
        }
    }
}
