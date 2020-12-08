using CarDealershipInventory.Core.ApplicationServices;
using CarDealershipInventory.Core.ApplicationServices.Impl;
using CarDealershipInventory.Core.ApplicationServices.Validators;
using CarDealershipInventory.Core.ApplicationServices.Validators.Interfaces;
using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CarDealershipInventory.Test
{
    public class ModelServiceTest
    {
        private Mock<IModelRepository> repoMock;
        //private IModelValidator modelValidator;
        //private Mock<IManufacturerRepository> manuRepoMock;
        private List<Model> models = null;
        //private List<Manufacturer> manufacturers = null;

        public ModelServiceTest()
        {
            repoMock = new Mock<IModelRepository>();
            //manuRepoMock = new Mock<IManufacturerRepository>();
            //modelValidator = new ModelValidator(repoMock.Object, manuRepoMock.Object);
            repoMock.Setup(repo => repo.ReadAllModels()).Returns(() => models);
            repoMock.Setup(repo => repo.ReadModelById(It.IsAny<int>())).Returns((int id) => models.FirstOrDefault(m => m.ModelId == id));
            //manuRepoMock.Setup(repo => repo.ReadManufacturerById(It.IsAny<int>())).Returns((int id) => manufacturers.FirstOrDefault(m => m.ManufacturerId == id));
        }

        [Fact]
        public void CreateModelService_RepositoryIsNull_ExpectArgumentException()
        {
            ModelService modelService = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                modelService = new ModelService(null, null);

            });
            Assert.Equal("Model repository is missing", ex.Message);
            Assert.Null(modelService);
        }

        #region GetAllModels
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllModels(int count)
        {
            models = new List<Model>();
            for (int i = 0; i < count; i++)
            {
                models.Add(new Model { ModelId = i });
            }

            IModelService modelService = new ModelService(repoMock.Object, null);
            int result = modelService.GetAllModels().Count;
            Assert.Equal(result, models.Count);

            repoMock.Verify(repo => repo.ReadAllModels(), Times.Once);
        }

        [Fact]
        public void GetAllModels_ListIsNull_ExpectNullReferenceException()
        {
            IModelService modelService = new ModelService(repoMock.Object, null);

            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                modelService.GetAllModels();

            });
            Assert.Equal("Model list is null", ex.Message);
            Assert.Null(models);
        }
        #endregion

        #region GetModelById

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 5)]
        [InlineData(3, 3)]
        public void GetModelById(int modelId, int manufacturerId)
        {
            models = new List<Model>();

            models.Add(new Model { ModelId = modelId, ManufacturerId = manufacturerId });

            IModelService modelService = new ModelService(repoMock.Object, null);

            Model model = modelService.GetModelById(modelId);

            Assert.Equal(manufacturerId, model.ManufacturerId);
            Assert.Equal(modelId, model.ModelId);

            repoMock.Verify(repo => repo.ReadModelById(modelId), Times.Once);
        }

        [Fact]
        public void GetModelById_ModelIsNotFound_ExpectNullReferenceException()
        {
            models = new List<Model>();

            models.Add(new Model { ModelId = 1, ManufacturerId = 1 });
            models.Add(new Model { ModelId = 2, ManufacturerId = 2 });
            models.Add(new Model { ModelId = 3, ManufacturerId = 3 });

            IModelService modelService = new ModelService(repoMock.Object, null);

            Model model = null;
            int id = 4;

            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                model = modelService.GetModelById(id);

            });
            Assert.Equal("Model was not found", ex.Message);
            Assert.Null(model);

            repoMock.Verify(repo => repo.ReadModelById(id), Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetModelById_IdIsNegative_ExpectArgumentException(int idToSearchFor)
        {
            models = new List<Model>();

            models.Add(new Model { ModelId = 1, ManufacturerId = 1 });
            models.Add(new Model { ModelId = 2, ManufacturerId = 2 });
            models.Add(new Model { ModelId = 3, ManufacturerId = 3 });

            IModelService modelService = new ModelService(repoMock.Object, null);

            Model model = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                model = modelService.GetModelById(idToSearchFor);
            });
            Assert.Equal("Model ID must be a positive integer", ex.Message);
            Assert.Null(model);

            repoMock.Verify(repo => repo.ReadModelById(idToSearchFor), Times.Never);
        }

        #endregion

        #region DeleteModel
        [Fact]
        public void DeleteModel_ExistingModel()
        {
            models = new List<Model>();

            Model model = new Model { ModelId = 1 };

            models.Add(model);


            IModelService modelService = new ModelService(repoMock.Object, null);

            modelService.DeleteModel(model.ModelId);


            repoMock.Verify(repo => repo.RemoveModel(It.Is<int>(m => m == model.ModelId)), Times.Once);
        }

        [Fact]
        public void DeleteModel_ModelIdIsZeroOrNegative_ExpectArgumentException()
        {
            IModelService modelService = new ModelService(repoMock.Object, null);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                modelService.DeleteModel(-1);

            });

            Assert.Equal("ModelId cannot be less than 1", ex.Message);
            repoMock.Verify(repo => repo.RemoveModel(It.Is<int>(m => m == -1)), Times.Never);
        }

        [Fact]
        public void DeleteModel_NonExistingModel_ExpectInvalidOperationException()
        {
            models = new List<Model>();

            Model model = new Model { ModelId = 1 };

            IModelService modelService = new ModelService(repoMock.Object, null);

            repoMock.Setup(repo => repo.ReadModelById(It.Is<int>(m => m == model.ModelId))).Returns(() => null);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                modelService.DeleteModel(model.ModelId);

            });


            Assert.Equal("Attempted to remove non-existing model", ex.Message);
            repoMock.Verify(repo => repo.RemoveModel(It.Is<int>(m => m == model.ModelId)), Times.Never);
        }
        #endregion

        #region CreateModel

        [Theory]
        [InlineData(1, "Ceed", 1)]
        [InlineData(2, "Picanto", 2)]
        public void AddModel_ValidNonExistingModel(int id, string name, int manufacturerId)
        {
            Model model = new Model()
            {
                ModelId = id,
                Name = name,
                ManufacturerId = manufacturerId,
            };

            repoMock.Setup(repo => repo.ReadModelById(It.Is<int>(m => m == model.ModelId))).Returns(() => null);

            ModelService modelService = new ModelService(repoMock.Object, null);


            Model createdModel = modelService.CreateModel(model);


            repoMock.Verify(repo => repo.AddModel(It.Is<Model>(m => m == model)), Times.Once);
        }

        [Fact]
        public void AddModel_ModelIsNull_ExpectArgumentException()
        {
            ModelService modelService = new ModelService(repoMock.Object, null);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                modelService.CreateModel(null);
            });

            Assert.Equal("Model is missing", ex.Message);
            repoMock.Verify(repo => repo.AddModel(It.Is<Model>(m => m == null)), Times.Never);
        }

        [Fact]
        public void AddModel_ModelExists_ExpectInvalidOperationException()
        {
            models = new List<Model>();

            Model model = new Model()
            {
                ModelId = 1,
                Name = "Ceed",
                ManufacturerId = 1,
            };

            models.Add(model);

            ModelService modelService = new ModelService(repoMock.Object, null);


            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                modelService.CreateModel(model);
            });

            Assert.Equal("Model already exist", ex.Message);
            repoMock.Verify(repo => repo.AddModel(It.Is<Model>(s => s == model)), Times.Never);


            
        }
        #endregion
    
    }

}
