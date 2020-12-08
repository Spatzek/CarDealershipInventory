using CarDealershipInventory.Core.ApplicationServices.Validators.Interfaces;
using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices.Validators
{
    public class ModelValidator : IModelValidator
    {
        private IModelRepository _modelRepository;
        private IManufacturerRepository _manufacturerRepository;

        public ModelValidator(IModelRepository modelRepository, IManufacturerRepository manufacturerRepository)
        {
            _modelRepository = modelRepository;
            _manufacturerRepository = manufacturerRepository;
        }
        public void ValidateModel(Model model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentException("Model must have a name");
            }
            //if (model.ManufacturerId.HasValue && _manufacturerRepository.ReadManufacturerById(model.ManufacturerId.Value) == null)
            if (_manufacturerRepository.ReadManufacturerById(model.ManufacturerId) == null)
            {
                throw new ArgumentException("Manufacturer does not exist in the database");
            }

            List<Model> models = _modelRepository.ReadAllModels();
            foreach (var mod in models)
            {
                if (mod.ManufacturerId == model.ManufacturerId && mod.Name.ToLower().Trim().Equals(model.Name.ToLower().Trim()) && mod.ModelId != model.ModelId)
                {
                    throw new ArgumentException("Name is already used by another model of the same manufacturer");
                }
            }
        }
    }
}
