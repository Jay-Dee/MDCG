﻿using FluentAssertions;
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
    public class FxSpotMarketDataRepositoryTests {
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
        public async Task Add_SavesFxSpotMarketDataAsync_Correctly() {
            var dbContext = new MDCGDbContext(new DbContextOptionsBuilder<MDCGDbContext>().UseInMemoryDatabase(nameof(Add_SavesFxSpotMarketDataAsync_Correctly)).Options);
            var userRepo = ConstructRepository(dbContext);
            var stubUser = ConstructEntity(1);

            var addedUser = await userRepo.Add(stubUser);

            addedUser.Should().NotBeNull();
            addedUser.Should().BeEquivalentTo(stubUser);
            dbContext.FxSpotMarketDatas.Count().Should().Be(1);

            await userRepo.Delete(1);
        }

        [Fact]
        public async Task Delete_ForValidExistingFxSpotMarketData_DeletesSuccessfully() {
            var dbContext = new MDCGDbContext(new DbContextOptionsBuilder<MDCGDbContext>().UseInMemoryDatabase("Delete_ForValidExistingFxSpotMarketData_DeletesSuccessfully").Options);
            var userRepo = ConstructRepository(dbContext);
            var stubUser = ConstructEntity(1);

            await userRepo.Add(stubUser);
            dbContext.FxSpotMarketDatas.Count().Should().Be(1);

            var deletedUser = await userRepo.Delete(1);
            deletedUser.Should().NotBeNull();
            deletedUser.Should().BeEquivalentTo(stubUser);
            dbContext.Users.Count().Should().Be(0);
        }

        [Fact]
        public async Task Get_ForValidExistingFxSpotMarketDataId_ReturnsUserCorrectly() {
            var dbContext = new MDCGDbContext(new DbContextOptionsBuilder<MDCGDbContext>().UseInMemoryDatabase("InMemoryUsersDb").Options);
            var userRepo = ConstructRepository(dbContext);
            var stubUser = ConstructEntity(1);
            await userRepo.Add(stubUser);
            dbContext.FxSpotMarketDatas.Count().Should().Be(1);

            var extractedUser = await userRepo.Get(1);

            extractedUser.Should().NotBeNull();
            extractedUser.Should().BeEquivalentTo(stubUser);
        }

        [Fact]
        public async Task GetAll_ForValidExistingFxSpotMarketDatas_ReturnsAllUsersCorrectly() {
            var dbContext = new MDCGDbContext(new DbContextOptionsBuilder<MDCGDbContext>().UseInMemoryDatabase(nameof(GetAll_ForValidExistingFxSpotMarketDatas_ReturnsAllUsersCorrectly)).Options);
            var userRepo = ConstructRepository(dbContext);

            for (int userCounter = 1; userCounter <= 5; userCounter++) {
                var stubUser = ConstructEntity(userCounter);
                await userRepo.Add(stubUser);
            }
            dbContext.FxSpotMarketDatas.Count().Should().Be(5);

            var extractedUsers = await userRepo.GetAll();

            extractedUsers.Should().NotBeNull();
            extractedUsers.Count.Should().Be(5);
            for (int userCounter = 1; userCounter <= 5; userCounter++) {
                await userRepo.Delete(userCounter);
            }
        }

        [Fact (Skip = "Investigate AsNoTrackingWithIdentityResolution() behaviour")]
        public async Task Update_ForValidExistingFxSpotMarketData_UpdatesSuccessfully() {
            var dbContext = new MDCGDbContext(new DbContextOptionsBuilder<MDCGDbContext>().UseInMemoryDatabase("InMemoryUsersDb").Options);
            var userRepo = ConstructRepository(dbContext);
            var stubUser = ConstructEntity(1);

            await userRepo.Add(stubUser);
            dbContext.FxSpotMarketDatas.Count().Should().Be(1);

            var userToUpdate = ConstructEntity(1);
            var updatedUser = await userRepo.Update(userToUpdate);
            updatedUser.Should().NotBeNull();
            updatedUser.Should().BeEquivalentTo(userToUpdate);
            dbContext.Users.Count().Should().Be(1);
        }

        public FxSpotMarketDataRepository ConstructRepository(MDCGDbContext dbContext) {
            return new FxSpotMarketDataRepository(dbContext);
        }

        public FxSpotMarketData ConstructEntity(int id) {
            return new FxSpotMarketData { Id = id, BaseCurrency = "GBP", CounterCurrency = "INR", Ask = 10, Mid = 100, Bid = 110, BusinesssDate = DateTime.Today};
        }
    }
}
