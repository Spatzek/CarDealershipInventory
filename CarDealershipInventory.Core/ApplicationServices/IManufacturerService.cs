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
        public Manufacturer CreateManufacturer(Manufacturer manufacturer);
        public Manufacturer EditManufacturer(Manufacturer manufacturer);
        public Manufacturer DeleteManufacturer(int id);
    }
}
