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
                .Where(m => m.Name != "Default")
                .ToList();
        }

        public Manufacturer ReadManufacturerById(int id)
        {
            return _ctx.Manufacturers
                .AsNoTracking()
                .Include(m => m.Models.Where(m => m.Name != "Default"))
                .FirstOrDefault(m => m.ManufacturerId == id);
        }

        public Manufacturer AddManufacturer(Manufacturer manufacturer)
        {
            // not fully implemented, should also add default model
            var entry = _ctx.Add(manufacturer);
            _ctx.SaveChanges();
            return entry.Entity;
        }

        public Manufacturer UpdateManufacturer(Manufacturer manufacturer)
        {
            throw new NotImplementedException();
        }

        public Manufacturer RemoveManufacturer(int id)
        {
            // not fully implemented, should also set cars to default manifacturer and default model
            var entry = _ctx.Remove(new Manufacturer { ManufacturerId = id });
            _ctx.SaveChanges();
            return entry.Entity;
        }        
    }
}