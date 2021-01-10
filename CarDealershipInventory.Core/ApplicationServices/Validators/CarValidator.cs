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
                throw new ArgumentException("Bil til validering mangler");
            }
            ValidateCarId(car.CarId, isUpdate);
            ValidateCarModel(car.ModelId);
            ValidateNumberIsNonNegative(car.Key, "Nøgle");
            ValidateTextIsNotNullOrEmpty(car.Location, "Lokation");
            ValidateProductionYear(car.ProductionYear);
            ValidateDateIsNotInFuture(car.LastInspection, "Sidste synsdato");
            ValidateDateIsNotBeforeProductionYear(car.LastInspection, car.ProductionYear, "Sidste synsdato");
            ValidateNumberIsNonNegative(car.Kilometers, "Kilometertal");            
            ValidateTextIsNotNullOrEmpty(car.LicensePlate, "Nummerplade");
            ValidateDateIsNotNull(car.DateOfPurchase, "Købsdato");
            ValidateDateIsNotBeforeProductionYear(car.DateOfPurchase, car.ProductionYear, "Købsdato");
            ValidateDateIsNotInFuture(car.DateOfPurchase, "Købsdato");
            ValidateNumberIsNonNegative(car.PurchasePrice, "Købspris");
            ValidateNumberIsNonNegative(car.CurrentPrice, "Nu-pris");
            ValidateDateOfSaleIsNotBeforeDateOfPurchase(car.DateOfPurchase, car.DateOfSale);
            ValidateNumberIsNonNegative(car.SoldPrice, "Salgspris");
            ValidateNumberIsNonNegative(car.VAT, "Moms");
        }

        public void ValidateCarId(int carId, bool isUpdate)
        {
            if (!isUpdate)
            {
                if (_carRepository.ReadCarById(carId) != null)
                {
                    throw new InvalidOperationException("Car ID can not match car which already exists");
                }
            }
            else
            {
                if (_carRepository.ReadCarById(carId) == null)
                {
                    throw new InvalidOperationException("Can not update car which does not exist");
                }
            }
            
        }

        public void ValidateCarModel(int modelId)
        {            
            if (_modelRepository.ReadModelById(modelId) == null)
            {
                throw new ArgumentException("Model blev ikke fundet");
            }
        }

        public void ValidateNumberIsNonNegative(double number, string property)
        {
            if (number < 0)
            {
                throw new ArgumentException($"{property} kan ikke være negativ");
            }
        }

        public void ValidateTextIsNotNullOrEmpty(string text, string property)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException($"{property} information mangler");
            }
        }

        public void ValidateDateIsNotNull(DateTime? date, string property)
        {
            if(date == null)
            {
                throw new ArgumentException($"{property} skal defineres");
            }
        }

        public void ValidateDateIsNotInFuture(DateTime? date, string property)
        {
            if (date.HasValue && date.Value.Date > DateTime.Today.Date)
            {
                throw new ArgumentException($"{property} kan ikke ligge i fremtiden");
            }
        }

        public void ValidateProductionYear(int year)
        {
            int firstCar = 1880; // oldest running automobile is from 1884...

            if (year < firstCar || year > DateTime.Now.Year)
            {
                throw new ArgumentException("Produktionsår skal ligge mellem 1880 og indeværende år");
            }
        }

        public void ValidateDateOfSaleIsNotBeforeDateOfPurchase(DateTime? purchaseDate, DateTime? saleDate)
        {
            if (purchaseDate.HasValue && saleDate.HasValue && purchaseDate.Value.Date > saleDate.Value.Date)
            {
                throw new ArgumentException("Salgsdato kan ikke ligge forud for købsdato");
            }
        }

        public void ValidateDateIsNotBeforeProductionYear(DateTime? date, int year, string property)
        {
            if(date.HasValue && date.Value.Year < year)
            {
                throw new ArgumentException($"{property} kan ikke ligge forud for produktionsår");
            }
        }


    }
}
