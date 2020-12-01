using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

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
            return _ctx.Cars
                .Include(c => c.Model)
                .ToList();
        }

        public Car ReadCarById(int id)
        {
            return _ctx.Cars
                .Include(c => c.Model)
                .FirstOrDefault(c => c.CarId == id);
        }
    }
}
