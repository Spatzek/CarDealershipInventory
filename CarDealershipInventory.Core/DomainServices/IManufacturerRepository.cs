using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.DomainServices
{
    public interface IManufacturerRepository
    {
        public List<Manufacturer> ReadAllManufacturers();
        public Manufacturer ReadManufacturerById(int id);
        public Manufacturer AddManufacturer(Manufacturer manufacturer);
        public Manufacturer UpdateManufacturer(Manufacturer manufacturer);
        public Manufacturer RemoveManufacturer(int id);

    }
}
