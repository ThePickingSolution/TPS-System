using Database.Warehouse.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Warehouse.Maps
{
    public class ItemStockEntityMap : IEntityTypeConfiguration<ItemStockEntity>
    {
        public void Configure(EntityTypeBuilder<ItemStockEntity> builder) {
            builder.ToTable("Warehouse.ItemStock");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.SKU).IsRequired();
            builder.Property(p => p.Details).HasMaxLength(Int32.MaxValue);
            builder.Property(p => p.StockCode).IsRequired().HasMaxLength(100);
            builder.Property(p => p.CreationDate).IsRequired();
            builder.Property(p => p.IsDeleted).IsRequired();
            builder.Property(p => p.DeletionDate);
        }
    }
}
