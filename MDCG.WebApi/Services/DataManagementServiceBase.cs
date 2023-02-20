using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.InteropServices;

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
            _repository.Add(entity);
            if (entity != null) {
                _memoryCache.Set(GetCacheKey(entity.Id), entity);
            }
            await _unitOfWork.CompleteAsync();
            return entity;
        }

        public virtual async Task<TEntity> Delete(int id) {
            var entity = await Get(id);
            _repository.Remove(entity);
            if(entity != null) {
                _memoryCache.Remove(GetCacheKey(id));
            }
            await _unitOfWork.CompleteAsync();
            return entity;
        }

        public virtual async Task<TEntity> Get(int id) {
            if (!_memoryCache.TryGetValue(GetCacheKey(id), out TEntity result)) {
                result = await _repository.GetById(id);
                _memoryCache.Set(GetCacheKey(id), result);
            }
            return result;
        }

        public virtual Task<IEnumerable<TEntity>> GetAll() {
            return Task.FromResult(_repository.GetAll());
        }

        public virtual async Task<TEntity> Update(TEntity entity) {
            _repository.Update(entity);
            if (entity != null) {
                _memoryCache.Set(GetCacheKey(entity.Id), entity);
            }
            await _unitOfWork.CompleteAsync();

            return entity;
        }

        private string GetCacheKey(int id) {
            return $"{typeof(TEntity)}_{id}";
        }
    }
}
