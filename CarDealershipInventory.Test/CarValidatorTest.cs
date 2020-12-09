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
        public void CarIsNull_ExpectArgumentNullException()
        {
            car = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateCar(car);
            });
            Assert.Equal("Car to validate is missing", ex.Message);
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
        public void ValidateDateIsNotInFuture_DateOfPurchaseIsInFuture_ExpectArgumentException()
        {
            car.DateOfPurchase = DateTime.Today.Date.AddDays(1);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateDateIsNotInFuture(car.DateOfPurchase, "Date of purchase");
            });
            Assert.Equal("Date of purchase can not be in the future", ex.Message);
        }



    }
}
