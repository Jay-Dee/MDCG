using MDCG.WebApi.Models;

namespace MDCG.WebApi.Services
{
    public interface IService<T> where T : class, IEntity {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}
