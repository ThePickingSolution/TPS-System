using Database.Picking.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Picking.Maps
{
    class PickingItemEntityMap : IEntityTypeConfiguration<PickingItemEntity>
    {
        public void Configure(EntityTypeBuilder<PickingItemEntity> builder)
        {
            builder.ToTable("Picking.PickingItem");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(200);
            builder.Property(p => p.SKU).IsRequired();
            builder.HasMany(p => p.Details).WithOne(p => p.PickingItem).HasForeignKey(fk => fk.PickingItem_Id);
        }
    }
}
