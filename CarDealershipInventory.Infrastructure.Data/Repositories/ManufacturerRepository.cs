using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Infrastructure.Data.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly CarDealershipInventoryContext _ctx;

        public ManufacturerRepository(CarDealershipInventoryContext ctx)
        {
            _ctx = ctx;
        }

        public List<Manufacturer> ReadAllManufacturers()
        {
            throw new NotImplementedException();
        }

        public Manufacturer ReadManufacturerById(int id)
        {
            throw new NotImplementedException();
        }

        public Manufacturer RemoveManufacturer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
