using CarDealershipInventory.Core.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using CarDealershipInventory.Core;
using Xunit;
using CarDealershipInventory.Core.ApplicationServices.Impl;
using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.ApplicationServices;

namespace CarDealershipInventory.Test
{
    public class CarServiceTest
    {
        private Mock<ICarRepository> repoMock;
        private List<Car> cars = null;

        public CarServiceTest()
        {
            repoMock = new Mock<ICarRepository>();
            repoMock.Setup(repo => repo.ReadAllCars()).Returns(() => cars);
        }

        [Fact]
        public void CreateCarService_RepositoryIsNull_ExpectArgumentException()
        {
            CarService carService = null;

            var ex = Assert.Throws<ArgumentException>(() =>
              {
                  carService = new CarService(null);

              });
            Assert.Equal("Car repository is missing", ex.Message);
            Assert.Null(carService);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllCars(int count)
        {
            cars = new List<Car>();
            for (int i = 0; i < count; i++)
            {
                cars.Add(new Car { CarId = i });
            }

            ICarService carService = new CarService(repoMock.Object);
            int result = carService.GetAllCars().Count;
            Assert.Equal(result, cars.Count);
            repoMock.Verify(repo => repo.ReadAllCars(), Times.Once);
        }

        [Fact]
        public void GetAllCars_ListIsNull_ExpectNullReferenceException()
        {
            ICarService carService = new CarService(repoMock.Object);

            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                carService.GetAllCars();

            });
            Assert.Equal("Car list is null", ex.Message);
            Assert.Null(cars);
        }
    }
}
