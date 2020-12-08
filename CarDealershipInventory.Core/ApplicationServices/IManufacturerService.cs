using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices
{
    public interface IManufacturerService
    {
        public List<Manufacturer> GetAllManufacturers();
        public Manufacturer GetManufacturerById(int id);
        public Manufacturer DeleteManufacturer(int id);
    }
}
