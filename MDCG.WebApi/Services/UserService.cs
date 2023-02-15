using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace MDCG.WebApi.Services
{
    public class UserService : DataManagementServiceBase<User> {
        public UserService(IRepository<User> repository, IUnitOfWork unitOfWork, IMemoryCache memoryCache) : base(repository, unitOfWork, memoryCache) {
        }
    }
}
