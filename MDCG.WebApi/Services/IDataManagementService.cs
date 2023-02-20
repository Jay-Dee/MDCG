using MDCG.WebApi.Models;

namespace MDCG.WebApi.Services
{
    public interface IDataManagementService<T> where T : class, IEntity {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}
