using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices
{
    public interface IModelService
    {
        public List<Model> GetAllModels();
        public Model GetModelById(int id);
        public Model DeleteModel(int id);
        public Model CreateModel(Model model);
        public Model EditModel(Model model);
    }
}
