using CarDealershipInventory.Core.ApplicationServices.Validators;
using CarDealershipInventory.Core.ApplicationServices.Validators.Interfaces;
using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CarDealershipInventory.Test
{
    public class CarValidatorTest
    {
        private Mock<ICarRepository> carRepoMock;
        private Mock<IModelRepository> modelRepoMock;
        private List<Car> cars = null;
        private List<Model> models = null;
        private ICarValidator validator;
        private Car car;

        public CarValidatorTest()
        {
            carRepoMock = new Mock<ICarRepository>();
            modelRepoMock = new Mock<IModelRepository>();
            carRepoMock.Setup(repo => repo.ReadAllCars()).Returns(() => cars);
            modelRepoMock.Setup(repo => repo.ReadModelById(It.IsAny<int>())).Returns((int id) => models.FirstOrDefault(c => c.ModelId == id));
            validator = new CarValidator(modelRepoMock.Object, carRepoMock.Object);
            car = new Car();
        }

        [Fact]
        public void CarIsNull_ExpectArgumentException()
        {
            car = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateCar(car);
            });
            Assert.Equal("Car to validate is missing", ex.Message);
            Assert.Null(car);
        }

        [Fact]
        public void ValidateCarModel_ModelIdNotInDB_ExpectArgumentException()
        {
            models = new List<Model>
            {
                new Model{ModelId = 1, Name = "Picanto"},
                new Model{ModelId = 2, Name = "Ceed"},
                new Model{ModelId = 3, Name = "c3"}
            };

            car.ModelId = 4;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateCarModel(car.ModelId);
            });
            Assert.Equal("Model with that ModelId was not found", ex.Message);

            modelRepoMock.Verify(repo => repo.ReadModelById(car.ModelId), Times.Once);
        }

        [Fact]
        public void ValidateNumberIsNonNegative_KeyIsNegative_ExpectArgumentException()
        {
            car.Key = -1;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateNumberIsNonNegative(car.Key, "Key number");
            });
            Assert.Equal("Key number can not be negative", ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateTextIsNotNullOrEmpty_LocationIsNullOrEmpty_ExpectArgumentException(string text)
        {
            car.Location = text;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateTextIsNotNullOrEmpty(car.Location, "Location");
            });
            Assert.Equal("Location information is missing or empty", ex.Message);
        }

        [Fact]
        public void ValidateDateIsNotInFuture_LastInspectionDateIsInFuture_ExpectArgumentException()
        {
            car.LastInspection = DateTime.Today.Date.AddDays(1);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateDateIsNotInFuture(car.LastInspection, "Last inspection date");
            });
            Assert.Equal("Last inspection date can not be in the future", ex.Message);
        }

        [Fact]
        public void ValidateNumberIsNonNegative_KilometersIsNegative_ExpectArgumentException()
        {
            car.Kilometers = -1;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateNumberIsNonNegative(car.Kilometers, "Kilometers");
            });
            Assert.Equal("Kilometers can not be negative", ex.Message);
        }

        [Fact]       
        public void ValidateProductionYear_ProductionYearTooEarly_ExpectArgumentException()
        {
            car.ProductionYear = 1879;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateProductionYear(car.ProductionYear);
            });
            Assert.Equal("Production year must in range between 1880 and this year", ex.Message);
        }

        [Fact]
        public void ValidateProductionYear_ProductionYearInFuture_ExpectArgumentException()
        {
            car.ProductionYear = DateTime.Today.Year + 1;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateProductionYear(car.ProductionYear);
            });
            Assert.Equal("Production year must in range between 1880 and this year", ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateTextIsNotNullOrEmpty_LicensePlateIsNullOrEmpty_ExpectArgumentException(string text)
        {
            car.LicensePlate = text;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateTextIsNotNullOrEmpty(car.LicensePlate, "License plate");
            });
            Assert.Equal("License plate information is missing or empty", ex.Message);
        }

        [Fact]
        public void ValidateDateIsNotNull_DateOfPurchaseIsNull_ExpectArgumentException()
        {
            car.DateOfPurchase = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateDateIsNotNull(car.DateOfPurchase, "Date of purchase");
            });
            Assert.Equal("Date of purchase must be defined", ex.Message);
        }

        [Fact]
        public void ValidateDateIsNotInFuture_DateOfPurchaseIsInFuture_ExpectArgumentException()
        {
            car.DateOfPurchase = DateTime.Today.Date.AddDays(1);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateDateIsNotInFuture(car.DateOfPurchase, "Date of purchase");
            });
            Assert.Equal("Date of purchase can not be in the future", ex.Message);
        }

        [Fact]
        public void ValidateNumberIsNonNegative_PurchasePriceIsNegative_ExpectArgumentException()
        {
            car.PurchasePrice = -0.01;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateNumberIsNonNegative(car.PurchasePrice, "Purchase price");
            });
            Assert.Equal("Purchase price can not be negative", ex.Message);
        }

        [Fact]
        public void ValidateNumberIsNonNegative_CurrentPriceIsNegative_ExpectArgumentException()
        {
            car.CurrentPrice = -0.01;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateNumberIsNonNegative(car.CurrentPrice, "Current price");
            });
            Assert.Equal("Current price can not be negative", ex.Message);
        }

        [Fact]
        public void ValidateDateOfSaleIsNotBeforeDateOfPurchase_DateOfSaleIsBeforeDatePurchase_ExpectArgumentException()
        {
            car.DateOfPurchase = DateTime.Today.Date;
            car.DateOfSale = DateTime.Today.Date.AddDays(-1);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateDateOfSaleIsNotBeforeDateOfPurchase(car.DateOfPurchase, car.DateOfSale);
            });
            Assert.Equal("Date of sale can not precede date of purchase", ex.Message);
        }

        [Fact]
        public void ValidateNumberIsNonNegative_SoldPriceIsNegative_ExpectArgumentException()
        {
            car.SoldPrice = -0.01;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateNumberIsNonNegative(car.SoldPrice, "Sold price");
            });
            Assert.Equal("Sold price can not be negative", ex.Message);
        }

        [Fact]
        public void ValidateNumberIsNonNegative_VATIsNegative_ExpectArgumentException()
        {
            car.VAT = -0.01;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateNumberIsNonNegative(car.VAT, "Value added tax");
            });
            Assert.Equal("Value added tax can not be negative", ex.Message);
        }




    }
}
