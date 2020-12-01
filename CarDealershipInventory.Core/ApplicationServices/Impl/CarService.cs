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

        public List<Car> GetAllCars()
        {
            List<Car> carList = _carRepository.ReadAllCars();

            if (carList == null)
            {
                throw new NullReferenceException("Car list is null");
            }
            return carList;
        }
    }
}
