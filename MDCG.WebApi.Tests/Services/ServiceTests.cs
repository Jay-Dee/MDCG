using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using MDCG.WebApi.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDCG.WebApi.Tests.Services {
    public abstract class ServiceTests<TEntity, TService> 
        where TEntity : class, IEntity
        where TService : IDataManagementService<TEntity> {
        [Fact]
        public void Constructor_WithValidArgs_DoesNotThrowException() {
            var mqRepository = new Mock<IRepository<TEntity>>();
            var mqUnitOfWork = new Mock<IUnitOfWork>();
            var mqMemoryCache = new Mock<IMemoryCache>();

            var userService = ConstructService(mqRepository, mqUnitOfWork, mqMemoryCache);
        }

        [Fact]
        public async Task Add_ForValidUser_AddsToRepoAndToCacheSuccessfullyAsync() {
            TEntity stubUserToAdd = ConstructEntity(1);
            var mqRepository = new Mock<IRepository<TEntity>>();
            mqRepository.Setup(_ => _.Add(stubUserToAdd)).Returns(Task.FromResult(stubUserToAdd));
            var mqUnitOfWork = new Mock<IUnitOfWork>();
            var mqCacheEntry = new Mock<ICacheEntry>();
            var mqMemoryCache = new Mock<IMemoryCache>();
            mqMemoryCache.Setup(_ => _.CreateEntry(It.IsAny<string>())).Returns(mqCacheEntry.Object);
            TService userService = ConstructService(mqRepository, mqUnitOfWork, mqMemoryCache);
            var userAdded = await userService.Add(stubUserToAdd);

            Assert.Equal(stubUserToAdd, userAdded);
            mqRepository.Verify(_ => _.Add(stubUserToAdd), Times.Once);
            mqMemoryCache.Verify(_ => _.CreateEntry($"MDCG.WebApi.Models.{(typeof(TEntity)).Name}_1"), Times.Once);
        }

        

        [Fact]
        public async Task Delete_ForValidUser_DeletesFromRepoAndCacheSuccessfully() {
            var stubUserToDelete = ConstructEntity(1);
            var mqRepository = new Mock<IRepository<TEntity>>();
            mqRepository.Setup(_ => _.Delete(1)).Returns(Task.FromResult(stubUserToDelete));
            var mqUnitOfWork = new Mock<IUnitOfWork>();
            var mqCacheEntry = new Mock<ICacheEntry>();
            var mqMemoryCache = new Mock<IMemoryCache>();
            mqMemoryCache.Setup(_ => _.Remove(It.IsAny<string>()));

            var userService = ConstructService(mqRepository, mqUnitOfWork, mqMemoryCache);
            var userDeleted = await userService.Delete(stubUserToDelete.Id);

            Assert.Equal(stubUserToDelete, userDeleted);
            mqRepository.Verify(_ => _.Delete(1), Times.Once);
            mqMemoryCache.Verify(_ => _.Remove($"MDCG.WebApi.Models.{(typeof(TEntity)).Name}_1"), Times.Once);
        }

        [Fact]
        public async Task GetWithCacheHit_ForValidUser_GetsSingleUserFromCacheSuccessfully() {
            object stubUserToGet = ConstructEntity(1);
            var mqRepository = new Mock<IRepository<TEntity>>();
            mqRepository.Setup(_ => _.Get(1)).Returns(Task.FromResult((TEntity)stubUserToGet));
            var mqUnitOfWork = new Mock<IUnitOfWork>();
            var mqCacheEntry = new Mock<ICacheEntry>();
            var mqMemoryCache = new Mock<IMemoryCache>();
            mqMemoryCache.Setup(_ => _.TryGetValue(It.IsAny<object>(), out stubUserToGet)).Returns(true);

            var userService = ConstructService(mqRepository, mqUnitOfWork, mqMemoryCache);
            var userExtracted = await userService.Get(((TEntity)stubUserToGet).Id);

            Assert.Equal(stubUserToGet, userExtracted);
            mqRepository.Verify(_ => _.Get(1), Times.Never);
            mqMemoryCache.Verify(_ => _.TryGetValue($"MDCG.WebApi.Models.{(typeof(TEntity)).Name}_1", out stubUserToGet), Times.Once);
        }

        [Fact]
        public async Task GetWithCacheMiss_ForValidUser_GetsSingleUserFromRepoSuccessfully() {
            object stubUserToGet = ConstructEntity(1);
            var mqRepository = new Mock<IRepository<TEntity>>();
            mqRepository.Setup(_ => _.Get(1)).Returns(Task.FromResult((TEntity)stubUserToGet));
            var mqUnitOfWork = new Mock<IUnitOfWork>();
            var mqCacheEntry = new Mock<ICacheEntry>();
            var mqMemoryCache = new Mock<IMemoryCache>();
            mqMemoryCache.Setup(_ => _.TryGetValue(It.IsAny<object>(), out stubUserToGet)).Returns(false);
            mqMemoryCache.Setup(_ => _.CreateEntry(It.IsAny<string>())).Returns(mqCacheEntry.Object);

            var userService = ConstructService(mqRepository, mqUnitOfWork, mqMemoryCache);
            var userExtracted = await userService.Get(((TEntity)stubUserToGet).Id);

            Assert.Equal(stubUserToGet, userExtracted);
            mqRepository.Verify(_ => _.Get(1), Times.Once);
            mqMemoryCache.Verify(_ => _.TryGetValue($"MDCG.WebApi.Models.{(typeof(TEntity)).Name}_1", out stubUserToGet), Times.Once);
            mqMemoryCache.Verify(_ => _.CreateEntry($"MDCG.WebApi.Models.{(typeof(TEntity)).Name}_1"), Times.Once);
        }

        [Fact]
        public async Task GetAll_FetchesAllFromRepoSuccessfully() {
            var stubUsers = new List<TEntity>() 
            {
                ConstructEntity(1), 
                ConstructEntity(2)
        };
            var mqRepository = new Mock<IRepository<TEntity>>();
            mqRepository.Setup(_ => _.GetAll()).Returns(Task.FromResult(stubUsers));
            var mqUnitOfWork = new Mock<IUnitOfWork>();
            var mqMemoryCache = new Mock<IMemoryCache>();


            var userService = ConstructService(mqRepository, mqUnitOfWork, mqMemoryCache);
            var usersExtracted = await userService.GetAll();

            Assert.Equal(stubUsers.Count, usersExtracted.Count);
            Assert.Equal(stubUsers, usersExtracted);
            mqRepository.Verify(_ => _.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Update_ForValidUser_UpdatesSingleUserFromRepoSuccessfully() {
            TEntity stubUserToUpdate = ConstructEntity(1);
            var mqRepository = new Mock<IRepository<TEntity>>();
            mqRepository.Setup(_ => _.Update(stubUserToUpdate)).Returns(Task.FromResult(stubUserToUpdate));
            var mqUnitOfWork = new Mock<IUnitOfWork>();
            var mqCacheEntry = new Mock<ICacheEntry>();
            var mqMemoryCache = new Mock<IMemoryCache>();
            mqMemoryCache.Setup(_ => _.CreateEntry(It.IsAny<string>())).Returns(mqCacheEntry.Object);

            var userService = ConstructService(mqRepository, mqUnitOfWork, mqMemoryCache);
            var userUpdated = await userService.Update(stubUserToUpdate);

            Assert.Equal(stubUserToUpdate, userUpdated);
            mqRepository.Verify(_ => _.Update(stubUserToUpdate), Times.Once);
            mqMemoryCache.Verify(_ => _.CreateEntry($"MDCG.WebApi.Models.{(typeof(TEntity)).Name}_1"), Times.Once);
        }

        public abstract TService ConstructService(Mock<IRepository<TEntity>> mqRepository, Mock<IUnitOfWork> mqUnitOfWork, Mock<IMemoryCache> mqMemoryCache);

        public abstract TEntity ConstructEntity(int id);
    }
}
