using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.DomainServices
{
    public interface IManufacturerRepository
    {
        public List<Manufacturer> ReadAllManufacturers();
    }
}
