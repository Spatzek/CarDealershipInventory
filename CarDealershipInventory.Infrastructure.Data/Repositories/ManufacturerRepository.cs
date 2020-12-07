using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _ctx.Manufacturers
                .AsNoTracking()
                .ToList();
        }

        public Manufacturer ReadManufacturerById(int id)
        {
            return _ctx.Manufacturers
                .AsNoTracking()
                .Include(m => m.Models)
                .FirstOrDefault(m => m.ManufacturerId == id);
        }

        public Manufacturer RemoveManufacturer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
