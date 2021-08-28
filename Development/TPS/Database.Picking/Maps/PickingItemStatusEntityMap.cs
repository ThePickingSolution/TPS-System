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
    class PickingItemStatusEntityMap : IEntityTypeConfiguration<PickingItemStatusEntity>
    {
        public void Configure(EntityTypeBuilder<PickingItemStatusEntity> builder) {
            builder.ToTable("Picking.PickingItemStatus");
            builder.HasKey(k => k.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();
            builder.Property(p => p.Status).IsRequired().HasMaxLength(50);

            builder.HasData(
                Enum.GetValues(typeof(ItemStatus))
                    .Cast<ItemStatus>()
                    .Select(s => new PickingItemStatusEntity() {
                        Id = (int)s,
                        Status = s.ToString()
                    })
                );
        }
    }
}
