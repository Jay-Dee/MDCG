using MDCG.WebApi.Data;
using System;

namespace MDCG.WebApi.Repository {
    public class UnitOfWork : IUnitOfWork {
        private readonly MDCGDbContext _context;

        public UnitOfWork(MDCGDbContext context) {
            _context = context;
        }

        public async Task CompleteAsync() {
            await _context.SaveChangesAsync();
        }
    }
}
