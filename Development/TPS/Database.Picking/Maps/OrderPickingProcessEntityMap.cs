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
    class OrderPickingProcessEntityMap : IEntityTypeConfiguration<OrderPickingProcessEntity>
    {
        public void Configure(EntityTypeBuilder<OrderPickingProcessEntity> builder)
        {
            builder.ToTable("Picking.OrderPickingProcess");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Operator).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Date).IsRequired();

            builder.Property(p => p.Sector);
            builder.Property(p => p.Container);

            builder.HasOne(p => p.OrderPicking).WithMany(m => m.Processes).HasForeignKey(fk => fk.OrderPicking_Id).IsRequired();
            builder.HasOne(p => p.Status).WithMany().HasForeignKey(fk => fk.Status_Id).IsRequired();
        }
    }
}
