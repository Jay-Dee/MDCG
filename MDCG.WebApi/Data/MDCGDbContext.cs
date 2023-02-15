using MDCG.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MDCG.WebApi.Data {
    public class MDCGDbContext : DbContext, IMDCGDbContext {
        public MDCGDbContext(DbContextOptions<MDCGDbContext> options)
            : base(options) {
        }

        public DbSet<User> Users { get; set; } = default!;

        public DbSet<FxSpotMarketData> FxSpotMarketDatas { get; set; } = default!;

        public DbSet<EquitySpotMarketData> EquitySpotMarketDatas { get; set; } = default!;
    }
}
