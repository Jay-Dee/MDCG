using MDCG.WebApi.Data;
using MDCG.WebApi.Models;

namespace MDCG.WebApi.Repository
{
    public class EquitySpotMarketDataRepository : EfCoreRepositoryBase<EquitySpotMarketData, MDCGDbContext>, IRepository<EquitySpotMarketData> {
        public EquitySpotMarketDataRepository(MDCGDbContext dbContext) : base(dbContext) {

        }
    }
}
