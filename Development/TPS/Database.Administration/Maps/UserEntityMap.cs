using Database.Administration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Administration.Maps
{
    class UserEntityMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Administration.User");
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Active);
            builder.Property(k => k.CreatedOn).IsRequired();
            builder.Property(k => k.DeletedOn);

            builder.Property(p => p.Username).IsRequired().HasMaxLength(100);
            builder.HasIndex(p => p.Username).IsUnique();

            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
        }
    }
}
