using Business.Domain.Picking;
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
    class OrderPickingStatusEntityMap : IEntityTypeConfiguration<OrderPickingStatusEntity>
    {
        public void Configure(EntityTypeBuilder<OrderPickingStatusEntity> builder)
        {
            builder.ToTable("Picking.OrderPickingStatus");
            builder.HasKey(k => k.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();
            builder.Property(p => p.Status).IsRequired().HasMaxLength(50);

            builder.HasData(
                Enum.GetValues(typeof(PickingStatus))
                    .Cast<PickingStatus>()
                    .Select(s => new OrderPickingStatusEntity()
                    {
                        Id = (int)s,
                        Status = s.ToString()
                    })
                );
        }
    }
}
