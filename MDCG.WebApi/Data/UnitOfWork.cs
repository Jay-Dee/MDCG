using Microsoft.EntityFrameworkCore;
using System;

namespace MDCG.WebApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MDCGDbContext _context;

        public UnitOfWork(MDCGDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
