using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices
{
    public interface ICarService
    {
        public Car CreateCar(Car car);
        public List<Car> GetAllCars();
        public Car GetCarById(int id);
        public Car EditCar(Car car);
        public Car DeleteCar(int carId);

    }
}
