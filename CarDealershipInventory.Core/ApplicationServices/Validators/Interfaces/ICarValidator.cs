using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices.Validators.Interfaces
{
    public interface ICarValidator
    {
        public void ValidateCar(Car car);

        public void ValidateCarModel(int modelId);

        public void ValidateNumberIsNonNegative(int number, string property);
        
    }
}
