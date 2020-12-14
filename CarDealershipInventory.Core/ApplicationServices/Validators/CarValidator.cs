using CarDealershipInventory.Core.ApplicationServices.Validators.Interfaces;
using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices.Validators
{
    public class CarValidator : ICarValidator
    {
        private ICarRepository _carRepository;
        private IModelRepository _modelRepository;
        public CarValidator(IModelRepository modelRepository, ICarRepository carRepository)
        {
            _carRepository = carRepository;
            _modelRepository = modelRepository;
        }

        public void ValidateCar(Car car, bool isUpdate)
        {
            if (car == null)
            {
                throw new ArgumentException("Car to validate is missing");
            }
            if(!isUpdate)
            {
                ValidateCarId(car.CarId);
            }
            ValidateCarModel(car.ModelId);
            ValidateNumberIsNonNegative(car.Key, "Key number");
            ValidateTextIsNotNullOrEmpty(car.Location, "Location");
            ValidateDateIsNotInFuture(car.LastInspection, "Last inspection date");
            ValidateNumberIsNonNegative(car.Kilometers, "Kilometers");
            ValidateProductionYear(car.ProductionYear);
            ValidateTextIsNotNullOrEmpty(car.LicensePlate, "License plate");
            ValidateDateIsNotNull(car.DateOfPurchase, "Date of purchase");
            ValidateDateIsNotInFuture(car.DateOfPurchase, "Date of purchase");
            ValidateNumberIsNonNegative(car.PurchasePrice, "Purchase price");
            ValidateNumberIsNonNegative(car.CurrentPrice, "Current price");
            ValidateDateOfSaleIsNotBeforeDateOfPurchase(car.DateOfPurchase, car.DateOfSale);
            ValidateNumberIsNonNegative(car.SoldPrice, "Sold price");
            ValidateNumberIsNonNegative(car.VAT, "Value added tax");
        }

        public void ValidateCarId(int carId)
        {
            if (_carRepository.ReadCarById(carId) != null)
            {
                throw new ArgumentException("This car ID is already in use by another car");
            }
        }

        public void ValidateCarModel(int modelId)
        {            
            if (_modelRepository.ReadModelById(modelId) == null)
            {
                throw new ArgumentException("Model with that ModelId was not found");
            }
        }

        public void ValidateNumberIsNonNegative(double number, string property)
        {
            if (number < 0)
            {
                throw new ArgumentException($"{property} can not be negative");
            }
        }

        public void ValidateTextIsNotNullOrEmpty(string text, string property)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException($"{property} information is missing or empty");
            }
        }

        public void ValidateDateIsNotNull(DateTime? date, string property)
        {
            if(date == null)
            {
                throw new ArgumentException($"{property} must be defined");
            }
        }

        public void ValidateDateIsNotInFuture(DateTime? date, string property)
        {
            if (date.HasValue && date.Value.Date > DateTime.Today.Date)
            {
                throw new ArgumentException($"{property} can not be in the future");
            }
        }

        public void ValidateProductionYear(int year)
        {
            int firstCar = 1880; // oldest running automobile is from 1884...

            if (year < firstCar || year > DateTime.Now.Year)
            {
                throw new ArgumentException("Production year must in range between 1880 and this year");
            }
        }

        public void ValidateDateOfSaleIsNotBeforeDateOfPurchase(DateTime? purchaseDate, DateTime? saleDate)
        {
            if (purchaseDate.HasValue && saleDate.HasValue && purchaseDate.Value.Date > saleDate.Value.Date)
            {
                throw new ArgumentException("Date of sale can not precede date of purchase");
            }
        }


    }
}
