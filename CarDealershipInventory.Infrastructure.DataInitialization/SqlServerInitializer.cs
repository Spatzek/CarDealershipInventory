using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using CarDealershipInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CarDealershipInventory.Infrastructure.DataInitialization
{
    public class SqlServerInitializer : IDataInitializer
    {
        private IAuthenticationHelper _authHelper;

        public SqlServerInitializer(IAuthenticationHelper authenticationHelper)
        {
            _authHelper = authenticationHelper;
        }

        public void Initialize(CarDealershipInventoryContext ctx)
        {
           /* ctx.Database.EnsureCreated();
        

            if (ctx.Cars.Any())
            {
                ctx.Database.ExecuteSqlRaw("DROP TABLE Cars");
            }
            if (ctx.Models.Any())
            {
                ctx.Database.ExecuteSqlRaw("DROP TABLE Models");
            }
            if (ctx.Manufacturers.Any())
            {
                ctx.Database.ExecuteSqlRaw("DROP TABLE Manufacturers");
            }
            if (ctx.Users.Any())
            {
                ctx.Database.ExecuteSqlRaw("DROP TABLE Users");
            }*/

            ctx.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS dbo.Cars, dbo.Models, dbo.Manufacturers, dbo.Users");
            ctx.Database.EnsureCreated();

            //users
            string password = "password";
            byte[] passwordHashAdmin, passwordSaltAdmin, passwordHashStandard, passwordSaltStandard;
            _authHelper.CreatePasswordHash(password, out passwordHashAdmin, out passwordSaltAdmin);
            _authHelper.CreatePasswordHash(password, out passwordHashStandard, out passwordSaltStandard);

            User admin = ctx.Users.Add(new User
            {
                Username = "AdminUser",
                PasswordHash = passwordHashAdmin,
                PasswordSalt = passwordSaltAdmin,
                IsAdmin = true
            }).Entity;

            User standard = ctx.Users.Add(new User
            {
                Username = "StandardUser",
                PasswordHash = passwordHashStandard,
                PasswordSalt = passwordSaltStandard,
                IsAdmin = false
            }).Entity;

            ctx.SaveChanges();

            // Fabrikanter
            Manufacturer placeholder = ctx.Manufacturers.Add(new Manufacturer
            {
                Name = "Default"
            }).Entity;
            ctx.SaveChanges();

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

            #region Placeholder Models
            Model placeholder1 = ctx.Models.Add(new Model
            {
                Name = "Default",
                ManufacturerId = 1
            }).Entity;
            ctx.SaveChanges();

            Model placeholder2 = ctx.Models.Add(new Model
            {
                Name = "Default",
                ManufacturerId = 2
            }).Entity;
            ctx.SaveChanges();

            Model placeholder3 = ctx.Models.Add(new Model
            {
                Name = "Default",
                ManufacturerId = 3
            }).Entity;
            ctx.SaveChanges();

            Model placeholder4 = ctx.Models.Add(new Model
            {
                Name = "Default",
                ManufacturerId = 4
            }).Entity;
            ctx.SaveChanges();

            Model placeholder5 = ctx.Models.Add(new Model
            {
                Name = "Default",
                ManufacturerId = 5
            }).Entity;
            ctx.SaveChanges();

            Model placeholder6 = ctx.Models.Add(new Model
            {
                Name = "Default",
                ManufacturerId = 6
            }).Entity;
            ctx.SaveChanges();

            Model placeholder7 = ctx.Models.Add(new Model
            {
                Name = "Default",
                ManufacturerId = 7
            }).Entity;
            ctx.SaveChanges();
            #endregion

            //// modeller
            Model picanto = ctx.Models.Add(new Model
            {
                Name = "Picanto",
                ManufacturerId = 2
            }).Entity;
            ctx.SaveChanges();

            Model ceed = ctx.Models.Add(new Model
            {
                Name = "Ceed",
                ManufacturerId = 2
            }).Entity;
            ctx.SaveChanges();

            Model fiesta = ctx.Models.Add(new Model
            {
                Name = "Fiesta",
                ManufacturerId = 3
            }).Entity;
            ctx.SaveChanges();

            Model hundredeogsyv = ctx.Models.Add(new Model
            {
                Name = "107",
                ManufacturerId = 4
            }).Entity;
            ctx.SaveChanges();

            Model corolla = ctx.Models.Add(new Model
            {
                Name = "Corolla",
                ManufacturerId = 2
            }).Entity;
            ctx.SaveChanges();

            Model golf = ctx.Models.Add(new Model
            {
                Name = "Golf",
                ManufacturerId = 2
            }).Entity;
            ctx.SaveChanges();

            Model clio = ctx.Models.Add(new Model
            {
                Name = "Clio",
                ManufacturerId = 2
            }).Entity;
            ctx.SaveChanges();



            Car car1 = ctx.Cars.Add(new Car
            {
                Key = 1,
                ModelId = 8,
                Location = "Hal 2",
                Kilometers = 155000,
                ProductionYear = 2010,
                LicensePlate = "AA11223",
                DateOfPurchase = DateTime.Parse("1/1/2020", new CultureInfo("da-DK")),
                PurchasePrice = 19000,
                CurrentPrice = 24000,
                DateOfSale = DateTime.Parse("1/2/2020", new CultureInfo("da-DK")),
                SoldPrice = 23000,
                VAT = 1000,
                IsSold = true,
                DaysInInventory = 30,
                LastInspection = DateTime.Parse("10/10/2018", new CultureInfo("da-DK"))
            }).Entity;

            Car car2 = ctx.Cars.Add(new Car
            {
                Key = 2,
                ModelId = 9,
                Location = "Hal 1",
                Kilometers = 150000,
                ProductionYear = 1999,
                LicensePlate = "AA11222",
                DateOfPurchase = DateTime.Parse("10/10/2020", new CultureInfo("da-DK")),
                PurchasePrice = 19000,
                CurrentPrice = 29000,
                DateOfSale = DateTime.Parse("10/11/2020", new CultureInfo("da-DK")),
                SoldPrice = 28500,
                VAT = 2500,
                IsSold = true,
                DaysInInventory = 30,
                LastInspection = DateTime.Parse("10/10/2018", new CultureInfo("da-DK"))
            }).Entity;
            ctx.SaveChanges();

            Car car3 = ctx.Cars.Add(new Car
            {
                Key = 3,
                ModelId = 11,
                Location = "Hal 3",
                Kilometers = 150000,
                ProductionYear = 1999,
                LicensePlate = "AB11222",
                DateOfPurchase = DateTime.Parse("5/10/2020", new CultureInfo("da-DK")),
                PurchasePrice = 19000,
                CurrentPrice = 29000,
                DateOfSale = DateTime.Parse("10/11/2020", new CultureInfo("da-DK")),
                SoldPrice = 28500,
                VAT = 2500,
                IsSold = true,
                DaysInInventory = 30,
                LastInspection = DateTime.Parse("10/10/2018", new CultureInfo("da-DK"))
            }).Entity;
            ctx.SaveChanges();

            Car car4 = ctx.Cars.Add(new Car
            {
                Key = 4,
                ModelId = 12,
                Location = "Hal 3",
                Kilometers = 150000,
                ProductionYear = 1999,
                LicensePlate = "AC11222",
                DateOfPurchase = DateTime.Parse("5/10/2020", new CultureInfo("da-DK")),
                PurchasePrice = 19000,
                CurrentPrice = 29000,
                DateOfSale = DateTime.Parse("10/11/2020", new CultureInfo("da-DK")),
                SoldPrice = 28500,
                VAT = 2500,
                IsSold = true,
                DaysInInventory = 30,
                LastInspection = DateTime.Parse("10/10/2018", new CultureInfo("da-DK"))
            }).Entity;
            ctx.SaveChanges();

            Car car5 = ctx.Cars.Add(new Car
            {
                Key = 5,
                ModelId = 13,
                Location = "Hal 3",
                Kilometers = 150000,
                ProductionYear = 1999,
                LicensePlate = "AD11222",
                DateOfPurchase = DateTime.Parse("5/10/2020", new CultureInfo("da-DK")),
                PurchasePrice = 19000,
                CurrentPrice = 29000,
                DateOfSale = DateTime.Parse("10/11/2020", new CultureInfo("da-DK")),
                SoldPrice = 28500,
                VAT = 2500,
                IsSold = true,
                DaysInInventory = 30,
                LastInspection = DateTime.Parse("10/10/2018", new CultureInfo("da-DK"))
            }).Entity;
            ctx.SaveChanges();

            Car car6 = ctx.Cars.Add(new Car
            {
                Key = 6,
                ModelId = 14,
                Location = "Hal 3",
                Kilometers = 150000,
                ProductionYear = 1999,
                LicensePlate = "AE11222",
                DateOfPurchase = DateTime.Parse("5/10/2020", new CultureInfo("da-DK")),
                PurchasePrice = 19000,
                CurrentPrice = 29000,
                DateOfSale = DateTime.Parse("10/11/2020", new CultureInfo("da-DK")),
                SoldPrice = 28500,
                VAT = 2500,
                IsSold = true,
                DaysInInventory = 30,
                LastInspection = DateTime.Parse("10/10/2018", new CultureInfo("da-DK"))
            }).Entity;
            ctx.SaveChanges();
        }
    }
}
