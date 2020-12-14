using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices.Validators.Interfaces
{
    public interface ICarValidator
    {
        public void ValidateCar(Car car, bool isUpdate);
        public void ValidateCarId(int carId);
        public void ValidateCarModel(int modelId);        
        public void ValidateNumberIsNonNegative(double number, string property);
        public void ValidateTextIsNotNullOrEmpty(string text, string property);
        public void ValidateDateIsNotInFuture(DateTime? date, string property);
        public void ValidateDateIsNotNull(DateTime? date, string property);
        public void ValidateProductionYear(int year);
        public void ValidateDateOfSaleIsNotBeforeDateOfPurchase(DateTime? purchaseDate, DateTime? saleDate);


    }
}
