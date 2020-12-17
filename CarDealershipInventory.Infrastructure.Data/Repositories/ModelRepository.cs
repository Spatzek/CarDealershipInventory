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

        public Model AddModel(Model model)
        {
            _ctx.Attach(model).State = EntityState.Added;
            _ctx.SaveChanges();
            return model;
        }

        public List<Model> ReadAllModels()
        {
            return _ctx.Models
                .AsNoTracking()
                .Include(m => m.Manufacturer)
                .Where(m => m.Name != "Default")
                .ToList();
        }

        public Model ReadModelById(int id)
        {
            return _ctx.Models
                .AsNoTracking()
                .Include(m => m.Manufacturer)
                .FirstOrDefault(m => m.ModelId == id);
        }

        public Model RemoveModel(int id)
        {
            Model model = ReadModelById(id);

            Manufacturer manu = _ctx.Manufacturers
                .AsNoTracking()
                .Include(m => m.Models)
                .FirstOrDefault(m => m.ManufacturerId == model.ManufacturerId);

            List<Car> cars = _ctx.Cars.Where(m => m.ModelId == id).ToList();
            foreach (Car car in cars)
            {
                car.ModelId = manu.Models.Find(x => x.Name.Equals("Default")).ModelId;
            }

            _ctx.Models.Remove(model);
            _ctx.SaveChanges();

            return model;
        }

        public Model UpdateModel(Model model)
        {
            var updatedModel = _ctx.Models.Update(model).Entity;
            _ctx.SaveChanges();

            return updatedModel;
        }
    }
}
