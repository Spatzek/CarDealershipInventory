using CarDealershipInventory.Core.ApplicationServices.Validators;
using CarDealershipInventory.Core.ApplicationServices.Validators.Interfaces;
using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            carRepoMock.Setup(repo => repo.ReadCarById(It.IsAny<int>())).Returns((int id) => cars.FirstOrDefault(c => c.CarId == id));
            validator = new CarValidator(modelRepoMock.Object, carRepoMock.Object);
            car = new Car();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void CarIsNull_ExpectArgumentException(bool isUpdate)
        {
            car = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateCar(car, isUpdate);
            });
            Assert.Equal("Bil til validering mangler", ex.Message);
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
            Assert.Equal("Model blev ikke fundet", ex.Message);

            modelRepoMock.Verify(repo => repo.ReadModelById(car.ModelId), Times.Once);
        }

        [Fact]
        public void ValidateNumberIsNonNegative_KeyIsNegative_ExpectArgumentException()
        {
            car.Key = -1;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateNumberIsNonNegative(car.Key, "Nøgle");
            });
            Assert.Equal("Nøgle kan ikke være negativ", ex.Message);
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
                validator.ValidateTextIsNotNullOrEmpty(car.Location, "Lokation");
            });
            Assert.Equal("Lokation information mangler", ex.Message);
        }

        [Fact]
        public void ValidateDateIsNotInFuture_LastInspectionDateIsInFuture_ExpectArgumentException()
        {
            car.LastInspection = DateTime.Today.Date.AddDays(1);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateDateIsNotInFuture(car.LastInspection, "Sidste synsdato");
            });
            Assert.Equal("Sidste synsdato kan ikke ligge i fremtiden", ex.Message);
        }

        [Fact]
        public void ValidateNumberIsNonNegative_KilometersIsNegative_ExpectArgumentException()
        {
            car.Kilometers = -1;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateNumberIsNonNegative(car.Kilometers, "Kilometertal");
            });
            Assert.Equal("Kilometertal kan ikke være negativ", ex.Message);
        }

        [Fact]       
        public void ValidateProductionYear_ProductionYearTooEarly_ExpectArgumentException()
        {
            car.ProductionYear = 1879;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateProductionYear(car.ProductionYear);
            });
            Assert.Equal("Produktionsår skal ligge mellem 1880 og indeværende år", ex.Message);
        }

        [Fact]
        public void ValidateProductionYear_ProductionYearInFuture_ExpectArgumentException()
        {
            car.ProductionYear = DateTime.Today.Year + 1;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateProductionYear(car.ProductionYear);
            });
            Assert.Equal("Produktionsår skal ligge mellem 1880 og indeværende år", ex.Message);
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
                validator.ValidateTextIsNotNullOrEmpty(car.LicensePlate, "Nummerplade");
            });
            Assert.Equal("Nummerplade information mangler", ex.Message);
        }

        [Fact]
        public void ValidateDateIsNotNull_DateOfPurchaseIsNull_ExpectArgumentException()
        {
            car.DateOfPurchase = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateDateIsNotNull(car.DateOfPurchase, "Købsdato");
            });
            Assert.Equal("Købsdato skal defineres", ex.Message);
        }

        [Fact]
        public void ValidateDateIsNotInFuture_DateOfPurchaseIsInFuture_ExpectArgumentException()
        {
            car.DateOfPurchase = DateTime.Today.Date.AddDays(1);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateDateIsNotInFuture(car.DateOfPurchase, "Købsdato");
            });
            Assert.Equal("Købsdato kan ikke ligge i fremtiden", ex.Message);
        }

        [Fact]
        public void ValidateNumberIsNonNegative_PurchasePriceIsNegative_ExpectArgumentException()
        {
            car.PurchasePrice = -0.01;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateNumberIsNonNegative(car.PurchasePrice, "Købspris");
            });
            Assert.Equal("Købspris kan ikke være negativ", ex.Message);
        }

        [Fact]
        public void ValidateNumberIsNonNegative_CurrentPriceIsNegative_ExpectArgumentException()
        {
            car.CurrentPrice = -0.01;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateNumberIsNonNegative(car.CurrentPrice, "Nu-pris");
            });
            Assert.Equal("Nu-pris kan ikke være negativ", ex.Message);
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
            Assert.Equal("Salgsdato kan ikke ligge forud for købsdato", ex.Message);
        }

        [Fact]
        public void ValidateNumberIsNonNegative_SoldPriceIsNegative_ExpectArgumentException()
        {
            car.SoldPrice = -0.01;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateNumberIsNonNegative(car.SoldPrice, "Salgspris");
            });
            Assert.Equal("Salgspris kan ikke være negativ", ex.Message);
        }

        [Fact]
        public void ValidateNumberIsNonNegative_VATIsNegative_ExpectArgumentException()
        {
            car.VAT = -0.01;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateNumberIsNonNegative(car.VAT, "Moms");
            });
            Assert.Equal("Moms kan ikke være negativ", ex.Message);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ValidateCarId_CreateCarWithExistingId_ExpectInvalidOperationException(int carId)
        {
            cars = new List<Car>
            {
                new Car{ CarId = 1},
                new Car{ CarId = 2},
                new Car{ CarId = 3}
            };

            car.CarId = carId;

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                validator.ValidateCarId(car.CarId, false);
            });
            Assert.Equal("Car ID can not match car which already exists", ex.Message);
        }

        [Theory]
        [InlineData(1)]        
        [InlineData(5)]
        public void ValidateCarId_UpdateNonExistingCar_ExpectInvalidOperationException(int carId)
        {
            cars = new List<Car>
            {
                new Car{ CarId = 2},
                new Car{ CarId = 3},
                new Car{ CarId = 4}
            };

            car.CarId = carId;

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                validator.ValidateCarId(car.CarId, true);
            });
            Assert.Equal("Can not update car which does not exist", ex.Message);
        }

        [Fact]
        public void ValidateDateIsNotBeforeProductionYear_DateOfPurchaseIsBeforeProductionYear_ExpectArgumentException()
        {
            car.CarId = 1;
            car.ProductionYear = 2021;
            car.DateOfPurchase = DateTime.Parse("31/12/2020", new CultureInfo("da-DK"));

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateDateIsNotBeforeProductionYear(car.DateOfPurchase, car.ProductionYear, "Købsdato");
            });
            Assert.Equal("Købsdato kan ikke ligge forud for produktionsår", ex.Message);
        }

        [Fact]
        public void ValidateDateIsNotBeforeProductionYear_LastInspectionIsBeforeProductionYear_ExpectArgumentException()
        {
            car.CarId = 1;
            car.ProductionYear = 2021;
            car.LastInspection = DateTime.Parse("31/12/2020", new CultureInfo("da-DK"));

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateDateIsNotBeforeProductionYear(car.LastInspection, car.ProductionYear, "Sidste synsdato");
            });
            Assert.Equal("Sidste synsdato kan ikke ligge forud for produktionsår", ex.Message);
        }



    }
}
