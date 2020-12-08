using CarDealershipInventory.Core.ApplicationServices.Validators;
using CarDealershipInventory.Core.ApplicationServices.Validators.Interfaces;
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
        private IModelValidator _modelValidator;

        public ModelService(IModelRepository modelRepository, IModelValidator modelValidator)
        {
            if(modelRepository == null)
            {
                throw new ArgumentException("Model repository is missing");
            }
            
            _modelRepository = modelRepository;
            _modelValidator = modelValidator;
        }

        public Model CreateModel(Model model)
        {
            List<Model> models = _modelRepository.ReadAllModels();

            if(model == null)
            {
                throw new ArgumentException("Model is missing");
            }
            
            if(models != null)
            { 
                foreach (Model m in models)
                {
                    if(model.ModelId == m.ModelId && model.Name == m.Name && model.ManufacturerId == m.ManufacturerId)
                    {
                        throw new InvalidOperationException("Model already exist");
                    }
                }
            }
            
            if(_modelValidator != null)
            {
                _modelValidator.ValidateModel(model);
            }

            return _modelRepository.AddModel(model);
        }

        public Model DeleteModel(int id)
        {
            if(id < 1)
            {
                throw new ArgumentException("ModelId cannot be less than 1");
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
            if(id <= 0)
            {
                throw new ArgumentException("Model ID must be a positive integer");
            }

            Model model = _modelRepository.ReadModelById(id);
            
            if(model == null)
            {
                throw new NullReferenceException("Model was not found");
            }
            return model;
        }
    }
}
