using FluentAssertions;
using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MDCG.WebApi.Tests.Repository {
    public abstract class RepositoryTests<TEntity, TRepository> 
        where TEntity : class, IEntity 
        where TRepository: IRepository<TEntity> {
        [Fact]
        public void Constructor_WithValidDbContext_DoesNotThrowException() {
            var dbContext = new MDCGDbContext(new DbContextOptionsBuilder<MDCGDbContext>().UseInMemoryDatabase(nameof(Constructor_WithValidDbContext_DoesNotThrowException)).Options);
            var userRepo = ConstructRepository(dbContext);
        }

        [Fact]
        public void Constructor_WithNullDbContext_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => new UserRepository(null));
        }

        [Fact]
        public async Task Add_SavesUserAsync_Correctly() {
            var dbContext = new MDCGDbContext(new DbContextOptionsBuilder<MDCGDbContext>().UseInMemoryDatabase(nameof(Add_SavesUserAsync_Correctly)).Options);
            var userRepo = ConstructRepository(dbContext);
            var stubUser = ConstructEntity(1);

            var addedUser = await userRepo.Add(stubUser);

            addedUser.Should().NotBeNull();
            addedUser.Should().BeEquivalentTo(stubUser);
            dbContext.Users.Count().Should().Be(1);

            await userRepo.Delete(1);
        }

        [Fact]
        public async Task Delete_ForValidExistingUser_DeletesSuccessfully() {
            var dbContext = new MDCGDbContext(new DbContextOptionsBuilder<MDCGDbContext>().UseInMemoryDatabase("InMemoryUsersDb").Options);
            var userRepo = ConstructRepository(dbContext);
            var stubUser = ConstructEntity(1);

            await userRepo.Add(stubUser);
            dbContext.Users.Count().Should().Be(1);

            var deletedUser = await userRepo.Delete(1);
            deletedUser.Should().NotBeNull();
            deletedUser.Should().BeEquivalentTo(stubUser);
            dbContext.Users.Count().Should().Be(0);
        }

        [Fact]
        public async Task Get_ForValidExistingUserId_ReturnsUserCorrectly() {
            var dbContext = new MDCGDbContext(new DbContextOptionsBuilder<MDCGDbContext>().UseInMemoryDatabase("InMemoryUsersDb").Options);
            var userRepo = ConstructRepository(dbContext);

            for (int userCounter = 1; userCounter <= 5; userCounter++) {
                var stubUser = ConstructEntity(userCounter);
            }
            dbContext.Users.Count().Should().Be(5);

            for (int userCounter = 1; userCounter <= 5; userCounter++) {
                var extractedUser = await userRepo.Get(userCounter);

                extractedUser.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task GetAll_ForValidExistingUsers_ReturnsAllUsersCorrectly() {
            var dbContext = new MDCGDbContext(new DbContextOptionsBuilder<MDCGDbContext>().UseInMemoryDatabase(nameof(GetAll_ForValidExistingUsers_ReturnsAllUsersCorrectly)).Options);
            var userRepo = ConstructRepository(dbContext);

            for (int userCounter = 1; userCounter <= 5; userCounter++) {
                var stubUser = ConstructEntity(userCounter);
                await userRepo.Add(stubUser);
            }
            dbContext.Users.Count().Should().Be(5);

            var extractedUsers = await userRepo.GetAll();

            extractedUsers.Should().NotBeNull();
            extractedUsers.Count.Should().Be(5);
            for (int userCounter = 1; userCounter <= 5; userCounter++) {
                await userRepo.Delete(userCounter);
            }
        }

        [Fact (Skip = "Investigate AsNoTrackingWithIdentityResolution() behaviour")]
        public async Task Update_ForValidExistingUser_UpdatesSuccessfully() {
            var dbContext = new MDCGDbContext(new DbContextOptionsBuilder<MDCGDbContext>().UseInMemoryDatabase("InMemoryUsersDb").Options);
            var userRepo = ConstructRepository(dbContext);
            var stubUser = ConstructEntity(1);

            await userRepo.Add(stubUser);
            dbContext.Users.Count().Should().Be(1);

            var userToUpdate = ConstructEntity(1);
            var updatedUser = await userRepo.Update(userToUpdate);
            updatedUser.Should().NotBeNull();
            updatedUser.Should().BeEquivalentTo(userToUpdate);
            dbContext.Users.Count().Should().Be(1);
        }

        public abstract TRepository ConstructRepository(MDCGDbContext dbContext);

        public abstract TEntity ConstructEntity(int id);
    }
}
