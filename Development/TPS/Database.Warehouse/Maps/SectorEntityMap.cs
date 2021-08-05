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
    public class SectorEntityMap : IEntityTypeConfiguration<SectorEntity>
    {
        public void Configure(EntityTypeBuilder<SectorEntity> builder) {
            builder.ToTable("Warehouse.Sector");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Code).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.CreationDate).IsRequired();
            builder.Property(p => p.IsDeleted).IsRequired();
            builder.Property(p => p.DeletionDate);

            builder.HasMany(p => p.Stocks).WithOne(o => o.Sector).HasForeignKey(fk => fk.Sector_Id);
        }
    }
}
