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
    class OrderPickingEntityMap : IEntityTypeConfiguration<OrderPickingEntity>
    {
        public void Configure(EntityTypeBuilder<OrderPickingEntity> builder)
        {
            builder.ToTable("Picking.OrderPicking");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(200);
            builder.Property(p => p.CreationDate).IsRequired();
            builder.HasMany(p => p.Details).WithOne(p => p.OrderPicking).HasForeignKey(fk => fk.OrderPicking_Id);
            builder.HasMany(p => p.Items).WithOne(p => p.OrderPicking).HasForeignKey(fk => fk.OrderPicking_Id);

        }
    }
}
