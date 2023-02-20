using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MDCG.WebApi.Repository
{
    public class EfCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity {

        private readonly MDCGDbContext _context;

        public EfCoreRepository(MDCGDbContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(TEntity entity) {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities) {
            _context.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression) {
            return _context.Set<TEntity>().Where(expression);
        }

        public IEnumerable<TEntity> GetAll() {
            return _context.Set<TEntity>().AsEnumerable();
        }

        public Task<TEntity> GetById(int id) {
            return _context.Set<TEntity>().FindAsync(id).AsTask();
        }

        public void Remove(TEntity entity) {
            _context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities) {
            _context.RemoveRange(entities);
        }

        public void Update(TEntity entity) {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
