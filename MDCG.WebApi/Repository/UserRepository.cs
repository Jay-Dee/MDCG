using MDCG.WebApi.Data;
using MDCG.WebApi.Models;

namespace MDCG.WebApi.Repository
{
    public class UserRepository : EfCoreRepositoryBase<User, MDCGDbContext>, IRepository<User> {
        public UserRepository(MDCGDbContext dbContext) : base(dbContext) {

        }
    }
}
