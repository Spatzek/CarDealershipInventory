using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Infrastructure.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDealershipInventoryContext _ctx;

        public CarRepository(CarDealershipInventoryContext ctx)
        {
            _ctx = ctx;
        }

        public List<Car> ReadAllCars()
        {
            throw new NotImplementedException();
        }
    }
}
