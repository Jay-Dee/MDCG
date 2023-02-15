using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using MDCG.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDCG.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : MDCGControllerBase<User, IDataManagementService<User>, IDataValidationService<User>> {
        public UsersController(IDataManagementService<User> dataManagementService, IDataValidationService<User> dataValidationService, ILogger<User> logger) : base(dataManagementService, dataValidationService, logger) {

        }
    }
}
