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
            repoMock.Setup(repo => repo.AddManufacturer(It.IsAny<Manufacturer>()))
                .Callback<Manufacturer>((manufacturer) => manufacturers.Add(manufacturer));
            repoMock.Setup(repo => repo.RemoveManufacturer(It.IsAny<int>()))
                .Callback<int>((id) => manufacturers.RemoveAll(m => m.ManufacturerId == id));
        }

        #region Constructor
        [Fact]
        public void CreateManufacturerService_RepositoryIsNull_ExpectArgumentException()
        {
            IManufacturerService service = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                service = new ManufacturerService(null, null);
            });

            Assert.Equal("Manufacturer repository is missing", ex.Message);
            Assert.Null(service);
        }
        #endregion

        #region GetAll
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

            IManufacturerService service = new ManufacturerService(repoMock.Object, null);
            int result = service.GetAllManufacturers().Count;
            Assert.Equal(result, manufacturers.Count);

            repoMock.Verify(repo => repo.ReadAllManufacturers(), Times.Once);
        }

        [Fact]
        public void GetAllManufacturers_ListIsNull_ExpectNullReferenceException()
        {
            manufacturers = null;
            List<Manufacturer> listOfManufacturers = null;
            IManufacturerService service = new ManufacturerService(repoMock.Object, null);

            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                listOfManufacturers = service.GetAllManufacturers();
            });
            Assert.Equal("List of manufacturers does not exist", ex.Message);
            Assert.Null(listOfManufacturers);

            repoMock.Verify(repo => repo.ReadAllManufacturers(), Times.Once);
        }
        #endregion

        #region GetById
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

            IManufacturerService service = new ManufacturerService(repoMock.Object, null);
            Manufacturer manufacturer = service.GetManufacturerById(id);

            Assert.Equal(manufacturer.ManufacturerId, id);
            Assert.Equal(manufacturer.Name, name);
            repoMock.Verify(repo => repo.ReadManufacturerById(It.IsAny<int>()), Times.Once);
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

            IManufacturerService service = new ManufacturerService(repoMock.Object, null);
            Manufacturer manufacturer = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                manufacturer = service.GetManufacturerById(id);
            });

            Assert.Equal("ID of the manufacturer to find must be a positive integer", ex.Message);
            Assert.Null(manufacturer);

            repoMock.Verify(repo => repo.ReadManufacturerById(It.IsAny<int>()), Times.Never);
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

            IManufacturerService service = new ManufacturerService(repoMock.Object, null);
            Manufacturer manufacturer = null;

            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                manufacturer = service.GetManufacturerById(id);
            });

            Assert.Equal("Manufacturer with this ID not found", ex.Message);
            Assert.Null(manufacturer);

            repoMock.Verify(repo => repo.ReadManufacturerById(It.IsAny<int>()), Times.Once);
        }
        #endregion

        #region Delete
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void DeleteManufacturer(int id)
        {
            manufacturers = new List<Manufacturer>()
            {
                new Manufacturer { ManufacturerId = 1 },
                new Manufacturer { ManufacturerId = 2 },
                new Manufacturer { ManufacturerId = 3 },
            };

            Assert.Equal(3, manufacturers.Count);
            Assert.True(manufacturers.Exists(m => m.ManufacturerId == id));

            IManufacturerService service = new ManufacturerService(repoMock.Object, null);

            Manufacturer manufacturer = service.DeleteManufacturer(id);

            Assert.Equal(2, manufacturers.Count);            
            Assert.False(manufacturers.Exists(m => m.ManufacturerId == id));

            repoMock.Verify(repo => repo.RemoveManufacturer(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void DeleteManufacturer_IdIsNotPositive_ExpectArgumentException(int id)
        {
            manufacturers = new List<Manufacturer>()
            {
                new Manufacturer { ManufacturerId = 1 },
                new Manufacturer { ManufacturerId = 2 },
                new Manufacturer { ManufacturerId = 3 },
            };

            Assert.Equal(3, manufacturers.Count);

            IManufacturerService service = new ManufacturerService(repoMock.Object, null);
            Manufacturer manufacturer = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                manufacturer = service.DeleteManufacturer(id);
            });

            Assert.Equal(3, manufacturers.Count);
            Assert.Null(manufacturer);           
            Assert.Equal("ID of the manufacturer to delete must be a positive integer", ex.Message);

            repoMock.Verify(repo => repo.RemoveManufacturer(It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void DeleteManufacturer_NoManufacturerWithId_ExpectInvalidOperationException(int id)
        {
            manufacturers = new List<Manufacturer>()
            {
                new Manufacturer { ManufacturerId = 2 },
                new Manufacturer { ManufacturerId = 3 },
                new Manufacturer { ManufacturerId = 4 },
            };

            Assert.Equal(3, manufacturers.Count);

            IManufacturerService service = new ManufacturerService(repoMock.Object, null);
            Manufacturer manufacturer = null;

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                manufacturer = service.DeleteManufacturer(id);
            });

            Assert.Equal(3, manufacturers.Count);
            Assert.Null(manufacturer);
            Assert.Equal("Can not delete manufacturer as no manufacturer with this ID can be found", ex.Message);

            repoMock.Verify(repo => repo.RemoveManufacturer(It.IsAny<int>()), Times.Never);
        }
        #endregion

        #region Create
        [Theory]
        [InlineData(1, "Ford")]
        [InlineData(3, "Renault")]
        public void CreateManufacturer(int manuId, string name)
        {
            manufacturers = new List<Manufacturer>();

            Manufacturer manufacturer = new Manufacturer
            {
                ManufacturerId = manuId,
                Name = name
            };

            IManufacturerService service = new ManufacturerService(repoMock.Object, null);

            service.CreateManufacturer(manufacturer);

            Assert.Equal(manufacturer.ManufacturerId, manufacturers[0].ManufacturerId);
            Assert.Equal(manufacturer.Name, manufacturers[0].Name);
            Assert.True(manufacturers.Count == 1);

            repoMock.Verify(repo => repo.AddManufacturer(It.IsAny<Manufacturer>()), Times.Once);
        }

        [Fact]
        public void CreateManufacturer_ManufacturerIdIsNegative_ExpectArgumentException()
        {
            manufacturers = new List<Manufacturer>();

            Manufacturer manufacturer = new Manufacturer
            {
                ManufacturerId = -1
            };

            IManufacturerService service = new ManufacturerService(repoMock.Object, null);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                service.CreateManufacturer(manufacturer);
            });
            Assert.Equal("Manufacturer ID for new car can not be negative", ex.Message);
            Assert.True(manufacturers.Count == 0);

            repoMock.Verify(repo => repo.AddManufacturer(It.IsAny<Manufacturer>()), Times.Never);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CreateManufacturer_ManufacturerWithIdAlreadyExists_ExpectInvalidOperationException(int manuId)
        {
            manufacturers = new List<Manufacturer>
            {
                new Manufacturer { ManufacturerId = 1},
                new Manufacturer { ManufacturerId = 2},
                new Manufacturer { ManufacturerId = 3}
            };

            Manufacturer manufacturer = new Manufacturer { ManufacturerId = manuId };

            IManufacturerService service = new ManufacturerService(repoMock.Object, null);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                service.CreateManufacturer(manufacturer);
            });
            Assert.Equal("Manufacturer ID can not match car which already exists", ex.Message);
            Assert.True(manufacturers.Count == 3);
            Assert.True(manufacturers.Where(m => m.ManufacturerId == manuId).ToList().Count == 1);

            repoMock.Verify(repo => repo.AddManufacturer(It.IsAny<Manufacturer>()), Times.Never);
        }
        #endregion



    }
}
