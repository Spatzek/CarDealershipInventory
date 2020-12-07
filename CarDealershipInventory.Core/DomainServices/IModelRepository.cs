using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.DomainServices
{
    public interface IModelRepository
    {
        public List<Model> ReadAllModels();
        public Model ReadModelById(int id);
        public Model RemoveModel(int id);
    }
}
