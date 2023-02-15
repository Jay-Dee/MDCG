using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace MDCG.WebApi.Services
{
    public  abstract class DataManagementServiceBase<TEntity> : IDataManagementService<TEntity> where TEntity : class, IEntity {
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;

        public DataManagementServiceBase(IRepository<TEntity> repository, IUnitOfWork unitOfWork, IMemoryCache memoryCache) {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        public virtual async Task<TEntity> Add(TEntity entity) {
            var result = await _repository.Add(entity);
            if(result != null) {
                _memoryCache.Set(GetCacheKey(entity.Id), entity);
            }
            
            return result;
        }

        public virtual async Task<TEntity> Delete(int id) {
            var result = await _repository.Delete(id);
            if(result != null) {
                _memoryCache.Remove(GetCacheKey(id));
            }
            return result;
        }

        public virtual async Task<TEntity> Get(int id) {
            if (!_memoryCache.TryGetValue(GetCacheKey(id), out TEntity result)) {
                result = await _repository.Get(id);
                _memoryCache.Set(GetCacheKey(id), result);
            }
            return result;
        }

        public virtual Task<List<TEntity>> GetAll() {
            return _repository.GetAll();
        }

        public virtual async Task<TEntity> Update(TEntity entity) {
            var result = await _repository.Update(entity);
            if (result != null) {
                _memoryCache.Set(GetCacheKey(entity.Id), result);
            }

            return result;
        }

        private string GetCacheKey(int id) {
            return $"{typeof(TEntity)}_{id}";
        }
    }
}
