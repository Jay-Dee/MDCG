using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace MDCG.WebApi.Services {
    public class FxSpotMarketDataService : IService<FxSpotMarketData> {
        private readonly IRepository<FxSpotMarketData> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FxSpotMarketDataService(IRepository<FxSpotMarketData> fxSpotMarketDataRepository, IUnitOfWork unitOfWork, IMemoryCache memoryCache) {
            this._userRepository = fxSpotMarketDataRepository;
            this._unitOfWork = unitOfWork;
        }

        public Task<FxSpotMarketData> Add(FxSpotMarketData entity) {
            return _userRepository.Add(entity);
        }

        public Task<FxSpotMarketData> Delete(int id) {
            return _userRepository.Delete(id);
        }

        public Task<FxSpotMarketData> Get(int id) {
            return _userRepository.Get(id);
        }

        public Task<List<FxSpotMarketData>> GetAll() {
            return _userRepository.GetAll();
        }

        public Task<FxSpotMarketData> Update(FxSpotMarketData entity) {
            return _userRepository.Update(entity);
        }
    }
}
