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
            // Fabrikanter
            Manufacturer kia = ctx.Manufacturers.Add(new Manufacturer
            {
                Name = "Kia"
            }).Entity;
            ctx.SaveChanges();

            Manufacturer ford = ctx.Manufacturers.Add(new Manufacturer
            {
                Name = "Ford"
            }).Entity;
            ctx.SaveChanges();

            Manufacturer peugeout = ctx.Manufacturers.Add(new Manufacturer
            {
                Name = "Peugeout"
            }).Entity;
            ctx.SaveChanges();

            Manufacturer toyota = ctx.Manufacturers.Add(new Manufacturer
            {
                Name = "Toyota"
            }).Entity;
            ctx.SaveChanges();

            Manufacturer vW = ctx.Manufacturers.Add(new Manufacturer
            {
                Name = "VW"
            }).Entity;
            ctx.SaveChanges();

            Manufacturer renault = ctx.Manufacturers.Add(new Manufacturer
            {
                Name = "Renault"
            }).Entity;
            ctx.SaveChanges();

            // modeller
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

            Model fiesta = ctx.Models.Add(new Model
            {
                Name = "Fiesta",
                Manufacturer = ford
            }).Entity;
            ctx.SaveChanges();

            Model hundredeogsyv = ctx.Models.Add(new Model
            {
                Name = "107",
                Manufacturer = peugeout
            }).Entity;
            ctx.SaveChanges();

            Model corolla = ctx.Models.Add(new Model
            {
                Name = "Corolla",
                Manufacturer = kia
            }).Entity;
            ctx.SaveChanges();

            Model golf = ctx.Models.Add(new Model
            {
                Name = "Golf",
                Manufacturer = kia
            }).Entity;
            ctx.SaveChanges();

            Model clio = ctx.Models.Add(new Model
            {
                Name = "Clio",
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
                LicensePlate = "AA11223",
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

            Car car3 = ctx.Cars.Add(new Car
            {
                Key = 3,
                Model = hundredeogsyv,
                Location = "Hal 3",
                Kilometers = 150000,
                ProductionYear = 1999,
                LicensePlate = "AB11222",
                DateOfPurchase = DateTime.Parse("5/10/2020"),
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

            Car car4 = ctx.Cars.Add(new Car
            {
                Key = 4,
                Model = corolla,
                Location = "Hal 3",
                Kilometers = 150000,
                ProductionYear = 1999,
                LicensePlate = "AC11222",
                DateOfPurchase = DateTime.Parse("5/10/2020"),
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

            Car car5 = ctx.Cars.Add(new Car
            {
                Key = 5,
                Model = golf,
                Location = "Hal 3",
                Kilometers = 150000,
                ProductionYear = 1999,
                LicensePlate = "AD11222",
                DateOfPurchase = DateTime.Parse("5/10/2020"),
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

            Car car6 = ctx.Cars.Add(new Car
            {
                Key = 6,
                Model = clio,
                Location = "Hal 3",
                Kilometers = 150000,
                ProductionYear = 1999,
                LicensePlate = "AE11222",
                DateOfPurchase = DateTime.Parse("5/10/2020"),
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
