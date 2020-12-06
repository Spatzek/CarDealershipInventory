using CarDealershipInventory.Core.ApplicationServices;
using CarDealershipInventory.Core.ApplicationServices.Impl;
using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using Moq;
using System;
using System.Collections.Generic;
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

    }

}
