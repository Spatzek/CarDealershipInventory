using CarDealershipInventory.Core.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using CarDealershipInventory.Core;
using Xunit;
using CarDealershipInventory.Core.ApplicationServices.Impl;

namespace CarDealershipInventory.Test
{
    public class CarServiceTest
    {
        private Mock<ICarRepository> repoMock;

        public CarServiceTest()
        {
            repoMock = new Mock<ICarRepository>();
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

    }
}
