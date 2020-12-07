using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices.Impl
{
    public class ManufacturerService : IManufacturerService
    {
        private IManufacturerRepository _manuRepository;

        public ManufacturerService(IManufacturerRepository manuRepository)
        {
            _manuRepository = manuRepository;
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            throw new NotImplementedException();
        }

        public Manufacturer GetManufacturerById(int id)
        {
            throw new NotImplementedException();
        }

        public Manufacturer DeleteManufacturer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
