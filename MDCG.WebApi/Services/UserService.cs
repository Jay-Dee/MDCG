using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace MDCG.WebApi.Services {
    public class UserService : IService<User> {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork, IMemoryCache memoryCache) {
            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
        }

        public Task<User> Add(User entity) {
            return _userRepository.Add(entity);
        }

        public Task<User> Delete(int id) {
            return _userRepository.Delete(id);
        }

        public Task<User> Get(int id) {
            return _userRepository.Get(id);
        }

        public Task<List<User>> GetAll() {
            return _userRepository.GetAll();
        }

        public Task<User> Update(User entity) {
            return _userRepository.Update(entity);
        }
    }
}
