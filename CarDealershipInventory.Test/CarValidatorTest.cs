using CarDealershipInventory.Core.ApplicationServices.Validators;
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

        public CarValidatorTest()
        {
            carRepoMock = new Mock<ICarRepository>();
            modelRepoMock = new Mock<IModelRepository>();
            carRepoMock.Setup(repo => repo.ReadAllCars()).Returns(() => cars);
            modelRepoMock.Setup(repo => repo.ReadModelById(It.IsAny<int>())).Returns((int id) => models.FirstOrDefault(c => c.ModelId == id));

        }

        [Fact]
        public void ModelIdNotInDB_ExpectArgumentException()
        {
            models = new List<Model>
            {
                new Model{ModelId = 1, Name = "Picanto"},
                new Model{ModelId = 2, Name = "Ceed"},
                new Model{ModelId = 3, Name = "c3"}
            };

            CarValidator validator = new CarValidator(modelRepoMock.Object, carRepoMock.Object);

            Car car = new Car { ModelId = 4 };

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateCar(car);

            });
            Assert.Equal("Model with that ModelId was not found", ex.Message);

            modelRepoMock.Verify(repo => repo.ReadModelById(car.ModelId), Times.Once);
        }




    }
}
