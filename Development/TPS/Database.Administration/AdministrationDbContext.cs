using Database.Administration.Entities;
using Database.Administration.Maps;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database.Administration
{
    public class AdministrationDbContext : DbContext
    {
        public AdministrationDbContext(DbContextOptions<AdministrationDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityMap());
        }
    }
}
