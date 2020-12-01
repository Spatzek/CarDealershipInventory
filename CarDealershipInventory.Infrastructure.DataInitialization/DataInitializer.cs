using CarDealershipInventory.Core.Entity;
using CarDealershipInventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Infrastructure.DataInitialization
{
    public class DataInitializer : IDataInitializer
    {
        public void Initialize(CarDealershipInventoryContext ctx)
        {
            ctx.Database.EnsureDeleted(); //ONLY in development //dont use when impl sql server
            ctx.Database.EnsureCreated();

            Manufacturer kia = ctx.Manufacturers.Add(new Manufacturer
            {
                Name = "Kia"
            }).Entity;
            ctx.SaveChanges();

            Model picanto = ctx.Models.Add(new Model
            {
                Name = "Picanto",
                Manufacturer = kia
            }).Entity;

            Model ceed = ctx.Models.Add(new Model
            {
                Name = "Ceed",
                Manufacturer = kia
            }).Entity;
            ctx.SaveChanges();

            Car car1 = ctx.Cars.Add(new Car
            {
                Key = 1,
                Model = picanto
            }).Entity;

            Car car2 = ctx.Cars.Add(new Car
            {
                Key = 2,
                Model = ceed
            }).Entity;
            ctx.SaveChanges();
        }
    }
}
