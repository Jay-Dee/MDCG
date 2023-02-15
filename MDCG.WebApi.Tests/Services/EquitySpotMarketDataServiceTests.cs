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
    public class EquitySpotMarketDataServiceTests : ServiceTests<EquitySpotMarketData, EquitySpotMarketDataService> {
        public override EquitySpotMarketData ConstructEntity(int id) {
            return new EquitySpotMarketData { Id = id, Bid = 1, Mid = 2, Ask = 3, BusinesssDate = DateTime.Today, Ticker = "GOOG", LongName = "Google Inc", Currency = "USD" };
        }

        public override EquitySpotMarketDataService ConstructService(Mock<IRepository<EquitySpotMarketData>> mqRepository, Mock<IUnitOfWork> mqUnitOfWork, Mock<IMemoryCache> mqMemoryCache) {
            return new EquitySpotMarketDataService(mqRepository.Object, mqUnitOfWork.Object, mqMemoryCache.Object);
        }
    }
}
