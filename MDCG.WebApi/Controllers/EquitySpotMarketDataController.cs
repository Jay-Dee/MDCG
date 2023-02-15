using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using MDCG.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDCG.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EquitySpotMarketDataController : MDCGControllerBase<EquitySpotMarketData, IDataManagementService<EquitySpotMarketData>, IDataValidationService<EquitySpotMarketData>> {
        public EquitySpotMarketDataController(IDataManagementService<EquitySpotMarketData> dataManagementService, IDataValidationService<EquitySpotMarketData> dataValidationService, ILogger<EquitySpotMarketData> logger) : base(dataManagementService, dataValidationService, logger) {

        }
    }
}
