using CarDealershipInventory.Core.ApplicationServices.Validators;
using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CarDealershipInventory.Test
{
    public class ManufacturerValidatorTest
    {
        private Mock<IManufacturerRepository> manuRepoMock;
        private List<Manufacturer> manufacturers;
        private Manufacturer manufacturer;
        private ManufacturerValidator validator;

        public ManufacturerValidatorTest()
        {
            manuRepoMock = new Mock<IManufacturerRepository>();
            manuRepoMock.Setup(repo => repo.ReadAllManufacturers()).Returns(() => manufacturers);
            manufacturers = new List<Manufacturer>()
            {
                new Manufacturer{ ManufacturerId = 1, Name = "Toyota"},
                new Manufacturer{ ManufacturerId = 2, Name = "Peugeot"},
                new Manufacturer{ ManufacturerId = 3, Name = "Skoda"}
            };
            manufacturer = new Manufacturer { ManufacturerId = 4 };
            manufacturers.Add(manufacturer);
            validator = new ManufacturerValidator(manuRepoMock.Object);
        }

        [Fact]
        public void ValidateManufacturer_ManufacturerIsNull_ExpectArgumentException()
        {
            manufacturer = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateManufacturer(manufacturer);
            });
            Assert.Equal("Manufacturer to validate is missing", ex.Message);
            Assert.Null(manufacturer);

            manuRepoMock.Verify(repo => repo.ReadAllManufacturers(), Times.Never);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ValidateManufacturer_WithInvalidName_ExpectArgumentException(string name)
        {
            manufacturer.Name = name;
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateManufacturer(manufacturer);
            });
            Assert.Equal("Manufacturer must have a name", ex.Message);

            manuRepoMock.Verify(repo => repo.ReadAllManufacturers(), Times.Never);
        }

        [Theory]
        [InlineData("Peugeot")]
        [InlineData("PEUGEOT")]
        [InlineData(" Peugeot ")]
        public void ValidateManufacturer_WithNameAlreadyUsed_ExpectArgumentException(string name)
        {
            manufacturer.Name = name;
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                validator.ValidateManufacturer(manufacturer);
            });
            Assert.Equal("Name is already used by another manufacturer", ex.Message);
           
            manuRepoMock.Verify(repo => repo.ReadAllManufacturers(), Times.Once);
        }

        // tests that a manufacturer can still be updated and keep the same name without throwing exception
        // because that name is already in the database
        [Theory]
        [InlineData("Peugeot")]
        [InlineData("PEUGEOT")]
        [InlineData(" Peugeot ")]
        public void ValidateManufacturer_AlreadyExistsWithSameName_ShouldPass(string name)
        {
            manufacturer.Name = name;
            manufacturer.ManufacturerId = 2;

            validator.ValidateManufacturer(manufacturer);

            manuRepoMock.Verify(repo => repo.ReadAllManufacturers(), Times.Once);
        }
    }
}
