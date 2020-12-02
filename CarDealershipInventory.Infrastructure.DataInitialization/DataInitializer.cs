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
                Model = picanto,
                Location = "Hal 2",
                Kilometers = 155000,
                ProductionYear = 2010,
                LicensePlate = "11223",
                DateOfPurchase = DateTime.Parse("1/1/2020"),
                PurchasePrice = 19000,
                CurrentPrice = 24000,
                DateOfSale = DateTime.Parse("1/2/2020"),
                SoldPrice = 23000,
                VAT = 1000,
                IsSold = true,
                DaysInInventory = 30,
                LastInspection = DateTime.Parse("10/10/2018")
            }).Entity;

            Car car2 = ctx.Cars.Add(new Car
            {
                Key = 2,
                Model = ceed,
                Location = "Hal 1",
                Kilometers = 150000,
                ProductionYear = 1999,
                LicensePlate = "AA11222",
                DateOfPurchase = DateTime.Parse("10/10/2020"),
                PurchasePrice = 19000,
                CurrentPrice = 29000,
                DateOfSale = DateTime.Parse("10/11/2020"),
                SoldPrice = 28500,
                VAT = 2500,
                IsSold = true,
                DaysInInventory = 30,
                LastInspection = DateTime.Parse("10/10/2018")
            }).Entity;
            ctx.SaveChanges();
        }
    }
    
}
