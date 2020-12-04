using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices.Validators
{
    public class CarValidator
    {
        private ICarRepository _carRepository;
        private IModelRepository _modelRepository;
        public CarValidator(IModelRepository modelRepository, ICarRepository carRepository)
        {
            _carRepository = carRepository;
            _modelRepository = modelRepository;
        }

        public void ValidateCar(Car car)
        {

        }
    }
}
