using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using MDCG.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDCG.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : MDCGControllerBase<User, UserService> {
        public UsersController(UserService service) : base(service) {

        }
    }
}
