using CarDealershipInventory.Core.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using CarDealershipInventory.Core;
using Xunit;
using System.Linq;
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
            repoMock.Setup(repo => repo.ReadCarById(It.IsAny<int>())).Returns((int id) => cars.FirstOrDefault(c => c.CarId == id));
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

            repoMock.Verify(repo => repo.ReadAllCars(), Times.Once);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 5)]
        [InlineData(3, 3)]
        public void GetCarById(int carId, int modelId)
        {
            cars = new List<Car>();

            cars.Add(new Car { CarId = carId, ModelId = modelId });
            cars.Add(new Car { CarId = carId, ModelId = modelId });
            cars.Add(new Car { CarId = carId, ModelId = modelId });

            ICarService carService = new CarService(repoMock.Object);

            Car car = carService.GetCarById(carId);

            Assert.Equal(modelId, car.ModelId);
            Assert.Equal(carId, car.CarId);

            repoMock.Verify(repo => repo.ReadCarById(carId), Times.Once);
        }

        [Fact]
        public void GetCarById_CarIsNotFound_ExpectNullReferenceException()
        {
            cars = new List<Car>();

            cars.Add(new Car { CarId = 1, ModelId = 1 });
            cars.Add(new Car { CarId = 2, ModelId = 2 });
            cars.Add(new Car { CarId = 3, ModelId = 3 });

            ICarService carService = new CarService(repoMock.Object);

            Car car = null;
            int carId = 4;

            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                car = carService.GetCarById(carId);

            });
            Assert.Equal("Car was not found", ex.Message);
            Assert.Null(car);

            repoMock.Verify(repo => repo.ReadCarById(carId), Times.Once);
        }
        #region DeleteModel
        [Fact]
        public void DeleteCar_ExistingCar()
        {
            cars = new List<Car>();

            Car car = new Car { CarId = 1 };

            cars.Add(car);


            ICarService carService = new CarService(repoMock.Object);

            carService.DeleteCar(car.CarId);


            repoMock.Verify(repo => repo.RemoveCar(It.Is<int>(m => m == car.CarId)), Times.Once);
        }
        [Fact]
        public void DeleteCar_CarIdIsZeroOrNegative_ExpectArgumentException()
        {
            ICarService carService = new CarService(repoMock.Object);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                carService.DeleteCar(-1);

            });

            Assert.Equal("CarId cannot be less than 1", ex.Message);
            repoMock.Verify(repo => repo.RemoveCar(It.Is<int>(m => m == -1)), Times.Never);
        }

        [Fact]
        public void DeleteCar_NonExistingCar_ExpectInvalidOperationException()
        {
            cars = new List<Car>();

            Car car = new Car { CarId = 1 };

            ICarService carService = new CarService(repoMock.Object);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                carService.DeleteCar(car.CarId);

            });


            Assert.Equal("Attempted to remove non-existing car", ex.Message);
            repoMock.Verify(repo => repo.RemoveCar(It.Is<int>(m => m == car.CarId)), Times.Never);
        }

    }
}
#endregion