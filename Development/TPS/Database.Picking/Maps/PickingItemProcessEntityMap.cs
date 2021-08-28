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
    class PickingItemProcessEntityMap : IEntityTypeConfiguration<PickingItemProcessEntity>
    {
        public void Configure(EntityTypeBuilder<PickingItemProcessEntity> builder) {
            builder.ToTable("Picking.PickingItemProcess");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Operator).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Date).IsRequired();

            builder.Property(p => p.Barcode);

            builder.HasOne(p => p.PickingItem).WithMany(m => m.Processes).HasForeignKey(fk => fk.PickingItem_Id).IsRequired();
            builder.HasOne(p => p.Status).WithMany().HasForeignKey(fk => fk.Status_Id).IsRequired();
        }
    }
}
