using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices.Impl
{
    public class CarService : ICarService
    {
        private ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            if(carRepository == null)
            {
                throw new ArgumentException("Car repository is missing");
            }    
            _carRepository = carRepository;
        }
        public Car DeleteCar(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("CarId cannot be less than 1");
            }
            else if (_carRepository.ReadCarById(id) == null)
            {
                throw new InvalidOperationException("Attempted to remove non-existing car");
            }
            return _carRepository.RemoveCar(id);
        }
        public List<Car> GetAllCars()
        {
            List<Car> carList = _carRepository.ReadAllCars();

            if (carList == null)
            {
                throw new NullReferenceException("Car list is null");
            }
            return carList;
        }

        public Car GetCarById(int id)
        {
            Car car = _carRepository.ReadCarById(id);

            if(car == null)
            {
                throw new NullReferenceException("Car was not found");
            }
            return car;
        }
    }
}
