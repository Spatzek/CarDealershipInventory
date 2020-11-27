using CarDealershipInventory.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Infrastructure.Data
{
    public class CarDealershipInventoryContext : DbContext
    {
        public CarDealershipInventoryContext(DbContextOptions<CarDealershipInventoryContext> opt)
            : base(opt) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Model)
                .WithMany(m => m.Cars)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Model>()
                .HasOne(m => m.Manufacturer)
                .WithMany(m => m.Models)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
