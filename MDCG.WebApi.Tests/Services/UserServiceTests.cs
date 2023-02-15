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
    public class UserServiceTests : ServiceTests<User, UserService> {
        public override User ConstructEntity(int id) {
            return new User() { Id = id, Name = $"User{id}", Email = "user@email.com", CanRead = true, CanWrite = true, IsActive = true};
        }

        public override UserService ConstructService(Mock<IRepository<User>> mqRepository, Mock<IUnitOfWork> mqUnitOfWork, Mock<IMemoryCache> mqMemoryCache) {
            return new UserService(mqRepository.Object, mqUnitOfWork.Object, mqMemoryCache.Object);
        }
    }
}
