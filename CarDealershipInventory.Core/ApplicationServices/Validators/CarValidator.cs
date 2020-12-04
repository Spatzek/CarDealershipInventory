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
            ValidateCarModel(car.ModelId);
            ValidateNumberIsNonNegative(car.Key, "Key number");
        }

        public void ValidateCarModel(int modelId)
        {            
            if (_modelRepository.ReadModelById(modelId) == null)
            {
                throw new ArgumentException("Model with that ModelId was not found");
            }
        }

        public void ValidateNumberIsNonNegative(int number, string property)
        {
            if (number < 0)
            {
                throw new ArgumentException($"{property} can not be negative");
            }
        }


    }
}
