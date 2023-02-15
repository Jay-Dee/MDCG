using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using MDCG.WebApi.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDCG.WebApi.Tests.Services {
    public class FxSpotMarketDataServiceTests : ServiceTests<FxSpotMarketData, FxSpotMarketDataService> {
        public override FxSpotMarketData ConstructEntity(int id) {
            return new FxSpotMarketData { Id = id, BaseCurrency = $"CC{id}", CounterCurrency = $"CC{id}", Bid = 1, Mid = 2, Ask = 3, BusinesssDate = DateTime.Today };
        }

        public override FxSpotMarketDataService ConstructService(Mock<IRepository<FxSpotMarketData>> mqRepository, Mock<IUnitOfWork> mqUnitOfWork, Mock<IMemoryCache> mqMemoryCache) {
            return new FxSpotMarketDataService(mqRepository.Object, mqUnitOfWork.Object, mqMemoryCache.Object);
        }
    }
}
