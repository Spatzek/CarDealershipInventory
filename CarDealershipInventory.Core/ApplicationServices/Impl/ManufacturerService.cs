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
            if (manuRepository == null)
            {
                throw new ArgumentException("Manufacturer repository is missing");
            }
            _manuRepository = manuRepository;
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            List<Manufacturer> manufacturers = _manuRepository.ReadAllManufacturers();
            if (manufacturers == null)
            {
                throw new NullReferenceException("List of manufacturers does not exist");
            }
            return manufacturers; 
        }

        public Manufacturer GetManufacturerById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID of the manufacturer to find must be a positive integer");
            }

            Manufacturer manufacturer = _manuRepository.ReadManufacturerById(id);
            if (manufacturer == null)
            {
                throw new NullReferenceException("Manufacturer with this ID not found");
            }
            return manufacturer;
        }

        public Manufacturer DeleteManufacturer(int id)
        {
            if(id <= 0 )
            {
                throw new ArgumentException("ID of the manufacturer to delete must be a positive integer");
            }
            if (_manuRepository.ReadManufacturerById(id) == null)
            {
                throw new InvalidOperationException("Can not delete manufacturer as no manufacturer with this ID can be found");
            }
            Manufacturer manufacturer = _manuRepository.RemoveManufacturer(id);
            return manufacturer;
        }
    }
}
