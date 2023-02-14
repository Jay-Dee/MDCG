using MDCG.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MDCG.WebApi.Data {
    public class MDCGDbContext : DbContext {
        public MDCGDbContext(DbContextOptions<MDCGDbContext> options)
            : base(options) {
        }

        public DbSet<User> User { get; set; } = default!;
    }
}
