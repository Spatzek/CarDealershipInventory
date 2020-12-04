using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices.Validators
{
    public class ManufacturerValidator
    {
        private IManufacturerRepository _manufacturerRepository;

        public ManufacturerValidator(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public void ValidateManufacturer(Manufacturer manufacturer)
        {
            if (string.IsNullOrEmpty(manufacturer.Name))
            {
                throw new ArgumentException("Manufacturer must have a name");
            }
            List<Manufacturer> manufacturers = _manufacturerRepository.ReadAllManufacturers();
            foreach (var manu in manufacturers)
            {
                if (manu.Name.ToLower().Trim().Equals(manufacturer.Name.ToLower().Trim()))
                {
                    throw new ArgumentException("Name is already used by another manufacturer");
                }
            }
        }

    }
}
