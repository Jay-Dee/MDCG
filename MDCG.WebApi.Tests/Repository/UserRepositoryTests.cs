using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDCG.WebApi.Tests.Repository {
    public class UserRepositoryTests {
        //[Fact]
        //public void Constructor_WithValidDbContext_DoesNotThrowException() {
        //    var stubContext = new MDCGDbContext(); 

        //    var userRepo = new UserRepository(stubContext);
        //}

        [Fact]
        public void Constructor_WithNullDbContext_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => new UserRepository(null));
        }

        [Fact]
        public async Task Add_SavesUserAsync_Correctly() {
            //var testUser = new User { Id = 1, Name = "test", Email = "user1@email.com", CanRead = true, CanWrite = true, IsActive = true };
            //var mqUsersSet = new Mock<DbSet<User>>();
            //var stubDbContext = new Mock<IMDCGDbContext>();
            //stubDbContext.Setup(_ => _.Users).Returns(mqUsersSet.Object);

            
            //var userRepo = new UserRepository(stubDbContext);
            //var savedUser = await userRepo.Add(testUser);

        }
    }
}
