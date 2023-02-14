using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace MDCG.WebApi.Services
{
    public  abstract class ServiceBase<TEntity> : IService<TEntity> where TEntity : class, IEntity {
        private readonly IRepository<TEntity> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceBase(IRepository<TEntity> repository, IUnitOfWork unitOfWork, IMemoryCache memoryCache) {
            this._userRepository = repository;
            this._unitOfWork = unitOfWork;
        }

        public virtual Task<TEntity> Add(TEntity entity) {
            return _userRepository.Add(entity);
        }

        public virtual Task<TEntity> Delete(int id) {
            return _userRepository.Delete(id);
        }

        public virtual Task<TEntity> Get(int id) {
            return _userRepository.Get(id);
        }

        public virtual Task<List<TEntity>> GetAll() {
            return _userRepository.GetAll();
        }

        public virtual Task<TEntity> Update(TEntity entity) {
            return _userRepository.Update(entity);
        }
    }
}
