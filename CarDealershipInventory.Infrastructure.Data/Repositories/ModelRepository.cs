using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealershipInventory.Infrastructure.Data.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly CarDealershipInventoryContext _ctx;

        public ModelRepository(CarDealershipInventoryContext ctx)
        {
            _ctx = ctx;
        }

        public List<Model> ReadAllModels()
        {
            return _ctx.Models
                .AsNoTracking()
                .Include(m => m.Manufacturer)
                .ToList();
        }

        public Model ReadModelById()
        {
            throw new NotImplementedException();
        }
    }
}
