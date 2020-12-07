using CarDealershipInventory.Core.ApplicationServices;
using CarDealershipInventory.Core.ApplicationServices.Impl;
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
    public class ManufacturerServiceTest
    {
        private Mock<IManufacturerRepository> repoMock;
        private List<Manufacturer> manufacturers = null;

        public ManufacturerServiceTest()
        {
            repoMock = new Mock<IManufacturerRepository>();
            repoMock.Setup(repo => repo.ReadAllManufacturers()).Returns(() => manufacturers);
            repoMock.Setup(repo => repo.ReadManufacturerById(It.IsAny<int>()))
                .Returns((int id) => manufacturers.FirstOrDefault(m => m.ManufacturerId == id));
            repoMock.Setup(repo => repo.RemoveManufacturer(It.IsAny<int>()))
                .Callback<int>((id) => manufacturers.Remove(new Manufacturer() { ManufacturerId = id }));
        }

        [Fact]
        public void CreateManufacturerService_RepositoryIsNull_ExpectArgumentException()
        {
            IManufacturerService service = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                service = new ManufacturerService(null);
            });

            Assert.Equal("Manufacturer repository is missing", ex.Message);
            Assert.Null(service);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllManufacturers(int count)
        {
            manufacturers = new List<Manufacturer>();
            for (int i = 0; i < count; i++)
            {
                manufacturers.Add(new Manufacturer { ManufacturerId = i });
            }

            IManufacturerService service = new ManufacturerService(repoMock.Object);
            int result = service.GetAllManufacturers().Count;
            Assert.Equal(result, manufacturers.Count);

            repoMock.Verify(repo => repo.ReadAllManufacturers(), Times.Once);
        }

        [Fact]
        public void GetAllManufacturers_ListIsNull_ExpectNullReferenceException()
        {
            manufacturers = null;
            List<Manufacturer> listOfManufacturers = null;
            IManufacturerService service = new ManufacturerService(repoMock.Object);

            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                listOfManufacturers = service.GetAllManufacturers();
            });
            Assert.Equal("List of manufacturers does not exist", ex.Message);
            Assert.Null(listOfManufacturers);

            repoMock.Verify(repo => repo.ReadAllManufacturers(), Times.Once);
        }

        [Theory]
        [InlineData(1, "Toyota")]
        [InlineData(2, "Peugeot")]
        [InlineData(3, "Ford")]
        public void GetManufacturerById(int id, string name)
        {
            manufacturers = new List<Manufacturer>()
            {
                new Manufacturer { ManufacturerId = 1, Name = "Toyota"},
                new Manufacturer { ManufacturerId = 2, Name = "Peugeot"},
                new Manufacturer { ManufacturerId = 3, Name = "Ford"},
            };

            IManufacturerService service = new ManufacturerService(repoMock.Object);
            Manufacturer manufacturer = service.GetManufacturerById(id);

            Assert.Equal(manufacturer.ManufacturerId, id);
            Assert.Equal(manufacturer.Name, name);
            repoMock.Verify(repo => repo.ReadManufacturerById(id), Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetManufacturerById_IdIsNotPositive_ExpectArgumentException(int id)
        {
            manufacturers = new List<Manufacturer>()
            {
                new Manufacturer { ManufacturerId = 1, Name = "Toyota"}                
            };

            IManufacturerService service = new ManufacturerService(repoMock.Object);
            Manufacturer manufacturer = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                manufacturer = service.GetManufacturerById(id);
            });

            Assert.Equal("Manufacturer ID must be a positive integer", ex.Message);
            Assert.Null(manufacturer);

            repoMock.Verify(repo => repo.ReadManufacturerById(id), Times.Never);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GetManufacturerById_NoManufacturerFound_ExpectNullReferenceException(int id)
        {
            manufacturers = new List<Manufacturer>()
            {
                new Manufacturer { ManufacturerId = 2, Name = "Toyota"},
                new Manufacturer { ManufacturerId = 3, Name = "Peugeot"},
                new Manufacturer { ManufacturerId = 4, Name = "Ford"},
            };

            IManufacturerService service = new ManufacturerService(repoMock.Object);
            Manufacturer manufacturer = null;

            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                manufacturer = service.GetManufacturerById(id);
            });

            Assert.Equal("Manufacturer with this ID does not exist", ex.Message);
            Assert.Null(manufacturer);

            repoMock.Verify(repo => repo.ReadManufacturerById(id), Times.Once);
        }


    }
}
