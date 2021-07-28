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
    class PickingItemDetailEntityMap : IEntityTypeConfiguration<PickingItemDetailEntity>
    {
        public void Configure(EntityTypeBuilder<PickingItemDetailEntity> builder)
        {
            builder.ToTable("Picking.PickingItemDetail" );
            builder.HasKey(k => new { k.PickingItem_Id, k.Name });
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Value).IsRequired().HasMaxLength(511);
        }
    }
}
