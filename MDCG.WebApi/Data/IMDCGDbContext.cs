using MDCG.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MDCG.WebApi.Data {
    public interface IMDCGDbContext {
        DbSet<EquitySpotMarketData> EquitySpotMarketDatas { get; set; }
        DbSet<FxSpotMarketData> FxSpotMarketDatas { get; set; }
        DbSet<User> Users { get; set; }
    }
}