using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using MDCG.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDCG.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FxSpotMarketDataController : MDCGControllerBase<FxSpotMarketData, IDataManagementService<FxSpotMarketData>, IDataValidationService<FxSpotMarketData>> {
        public FxSpotMarketDataController(IDataManagementService<FxSpotMarketData> dataManagementService, IDataValidationService<FxSpotMarketData> dataValidationService, ILogger<FxSpotMarketData> logger) : base(dataManagementService, dataValidationService, logger) {

        }
    }
}
