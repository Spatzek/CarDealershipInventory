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
                .AsNoTracking()
                .Include(c => c.Model)
                .ThenInclude(m => m.Manufacturer)
                .ToList();
        }

        public Car ReadCarById(int id)
        {
            return _ctx.Cars
                .AsNoTracking()
                .Include(c => c.Model)
                .ThenInclude(m => m.Manufacturer)
                .FirstOrDefault(c => c.CarId == id);
        }
        public Car RemoveCar(int id)
        {
            Car car = ReadCarById(id);

            car = null;

            List<Car> cars = _ctx.Cars.Where(m => m.CarId == id).ToList();
            foreach (Car _car in cars)
            {
                car = null;
                _car.CarId = 0;
            }


            _ctx.Cars.Remove(car);
            _ctx.SaveChanges();

            return car;
        }
    }
}
