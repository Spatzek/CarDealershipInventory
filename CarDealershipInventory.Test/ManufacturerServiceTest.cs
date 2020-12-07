using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            repoMock.Setup(repo => repo.ReadManufacturerById(It.IsAny<int>())).Returns((int id) => manufacturers.FirstOrDefault(m => m.ManufacturerId == id));
        }


    }
}
