using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices.Validators.Interfaces
{
    public interface IManufacturerValidator
    {
        public void ValidateManufacturer(Manufacturer manufacturer);
    }
}
