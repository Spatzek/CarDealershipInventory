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

        public Model DeleteModel(int id)
        {
            if(id == 0)
            {
                throw new ArgumentException("ModelId cannot be 0");
            }
            else if (_modelRepository.ReadModelById(id) == null)
            {
                throw new InvalidOperationException("Attempted to remove non-existing model");
            }
            return _modelRepository.RemoveModel(id);
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

        public Model GetModelById(int id)
        {
            Model model = _modelRepository.ReadModelById(id);
            
            if(model == null)
            {
                throw new NullReferenceException("Model was not found");
            }
            return model;
        }
    }
}
