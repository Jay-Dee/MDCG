using MDCG.WebApi.Models;
using System.Linq.Expressions;

namespace MDCG.WebApi.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity {
        Task<TEntity> GetById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
    }
}
