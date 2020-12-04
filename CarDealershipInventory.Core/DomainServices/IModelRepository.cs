using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.DomainServices
{
    public interface IModelRepository
    {
        public Model ReadModelById(int id);
        public List<Model> ReadAllModels();
        
    }

    
}
