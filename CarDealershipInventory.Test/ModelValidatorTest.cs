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
    public class ModelValidatorTest
    {
        private Mock<IModelRepository> modelRepoMock;
        private List<Model> models = null;
        private Model model = null;
        private ModelValidator validator = null;
        private Mock<IManufacturerRepository> manufacturerRepoMock;
        private List<Manufacturer> manufacturers = null;
        public ModelValidatorTest()
        {
            modelRepoMock = new Mock<IModelRepository>();
            modelRepoMock.Setup(repo => repo.ReadAllModels()).Returns(() => models);
            manufacturerRepoMock = new Mock<IManufacturerRepository>();
            manufacturerRepoMock.Setup(repo => repo.ReadManufacturerById(It.IsAny<int>())).Returns((int id) => manufacturers.FirstOrDefault(c => c.ManufacturerId == id));
            manufacturers = new List<Manufacturer>()
            {
                new Manufacturer{ Name = "Toyota", ManufacturerId = 1},
                new Manufacturer{ Name = "Peugeot", ManufacturerId = 2},
                new Manufacturer{ Name = "Skoda", ManufacturerId = 3}
            };
            model = new Model();
            validator = new ModelValidator(modelRepoMock.Object, manufacturerRepoMock.Object);
            models = new List<Model>()
            {
                new Model{ Name = "107", ManufacturerId = 2},
                new Model{ Name = "207", ManufacturerId = 2},
                new Model{ Name = "107", ManufacturerId = 3},
                new Model{ Name = "307", ManufacturerId = 2}
            };
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ValidateModel_WithInvalidName_ExpectArgumentException(string name)
        {
            model.Name = name;
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateModel(model);
            });
            Assert.Equal("Model must have a name", ex.Message);

            modelRepoMock.Verify(repo => repo.ReadAllModels(), Times.Never);
        }
        [Fact]
        public void ValidateModel_manufacturerNotInDatabase_ExpectArgumentException()
        {
            model.Name = "ceed";
            model.ManufacturerId = 4;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateModel(model);
            });
            Assert.Equal("Manufacturer does not exist in the database", ex.Message);
            manufacturerRepoMock.Verify(repo => repo.ReadManufacturerById(model.ManufacturerId), Times.Once);
        }
               
        [Theory]
        [InlineData("107")]
        [InlineData(" 107 ")]
        public void ValidateModel_WithNameAlreadyUsed_ExpectArgumentException(string name)
        {
            model.Name = name;
            model.ManufacturerId = 2;
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateModel(model);
            });
            Assert.Equal("Name is already used by another model of the same manufacturer", ex.Message);

            modelRepoMock.Verify(repo => repo.ReadAllModels(), Times.Once);
        }


    }

}
