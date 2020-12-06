using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices.Impl
{
    public class ModelService : IModelService
    {
        private IModelRepository _modelRepository;

        public ModelService(IModelRepository modelRepository)
        {
            if(modelRepository == null)
            {
                throw new ArgumentException("Model repository is missing");
            }
            
            _modelRepository = modelRepository;
        }


        public List<Model> GetAllModels()
        {
            List<Model> modelList = _modelRepository.ReadAllModels();

            if(modelList == null)
            {
                throw new NullReferenceException("Model list is null");
            }

            return modelList;
        }

        public Model GetModelById()
        {
            throw new NotImplementedException();
        }
    }
}
