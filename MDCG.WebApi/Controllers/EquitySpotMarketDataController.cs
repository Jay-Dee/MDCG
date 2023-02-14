using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using MDCG.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDCG.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EquitySpotMarketDataController : MDCGControllerBase<EquitySpotMarketData, IService<EquitySpotMarketData>> {
        public EquitySpotMarketDataController(IService<EquitySpotMarketData> service) : base(service) {

        }
    }
}
