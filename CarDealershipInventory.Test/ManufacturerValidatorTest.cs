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
        private List<Manufacturer> manufacturers = null;
        private Manufacturer manufacturer = null;
        private ManufacturerValidator validator = null;

        public ManufacturerValidatorTest()
        {
            manuRepoMock = new Mock<IManufacturerRepository>();
            manuRepoMock.Setup(repo => repo.ReadAllManufacturers()).Returns(() => manufacturers);
            manufacturers = new List<Manufacturer>()
            {
                new Manufacturer{ Name = "Toyota"},
                new Manufacturer{ Name = "Peugeot"},
                new Manufacturer{ Name = "Skoda"}
            };
            manufacturer = new Manufacturer();
            validator = new ManufacturerValidator(manuRepoMock.Object);
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
    }
}
