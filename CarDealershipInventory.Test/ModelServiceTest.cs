using CarDealershipInventory.Core.ApplicationServices;
using CarDealershipInventory.Core.ApplicationServices.Impl;
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
        private List<Model> models = null;

        public ModelServiceTest()
        {
            repoMock = new Mock<IModelRepository>();
            repoMock.Setup(repo => repo.ReadAllModels()).Returns(() => models);
            repoMock.Setup(repo => repo.ReadModelById(It.IsAny<int>())).Returns((int id) => models.FirstOrDefault(m => m.ModelId == id));
        }

        [Fact]
        public void CreateModelService_RepositoryIsNull_ExpectArgumentException()
        {
            ModelService modelService = null;

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                modelService = new ModelService(null);

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

            IModelService modelService = new ModelService(repoMock.Object);
            int result = modelService.GetAllModels().Count;
            Assert.Equal(result, models.Count);

            repoMock.Verify(repo => repo.ReadAllModels(), Times.Once);
        }

        [Fact]
        public void GetAllModels_ListIsNull_ExpectNullReferenceException()
        {
            IModelService modelService = new ModelService(repoMock.Object);

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

            IModelService modelService = new ModelService(repoMock.Object);

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

            IModelService modelService = new ModelService(repoMock.Object);

            Model model = null;

            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                model = modelService.GetModelById(4);

            });
            Assert.Equal("Model was not found", ex.Message);
            Assert.Null(model);
        }

        #endregion

        #region DeleteModel
        [Fact]
        public void DeleteModel_ExistingModel()
        {
            models = new List<Model>();

            Model model = new Model { ModelId = 1 };
            
            models.Add(model);


            IModelService modelService = new ModelService(repoMock.Object);

            modelService.DeleteModel(model.ModelId);


            repoMock.Verify(repo => repo.RemoveModel(It.Is<int>(m => m == model.ModelId)), Times.Once);
        }

        [Fact]
        public void DeleteModel_ModelIdIsZeroOrNegative_ExpectArgumentException()
        {
            IModelService modelService = new ModelService(repoMock.Object);

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

            IModelService modelService = new ModelService(repoMock.Object);

            //repoMock.Setup(repo => repo.GetModelById(It.Is<int>(m => m == model.ModelId))).Returns(() => null);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                modelService.DeleteModel(model.ModelId);

            });


            Assert.Equal("Attempted to remove non-existing model", ex.Message);
            repoMock.Verify(repo => repo.RemoveModel(It.Is<int>(m => m == model.ModelId)), Times.Never);
        }
        #endregion
    }

}
