using MDCG.WebApi.Data;
using MDCG.WebApi.Models;

namespace MDCG.WebApi.Repository {
    public class FxSpotMarketDataRepository : EfCoreRepositoryBase<FxSpotMarketData, MDCGDbContext>, IRepository<FxSpotMarketData> {
        public FxSpotMarketDataRepository(MDCGDbContext dbContext) : base(dbContext) {

        }
    }
}
