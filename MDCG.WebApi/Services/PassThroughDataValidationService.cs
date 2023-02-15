using MDCG.WebApi.Models;

namespace MDCG.WebApi.Services {
    public class PassThroughDataValidationService<TEntity> : IDataValidationService<TEntity> where TEntity : class, IEntity {
        public IValidationResult Validate(TEntity entity) {
            return new PassThroughValdiationResult();
        }
    }

    public class PassThroughValdiationResult : IValidationResult {

        public PassThroughValdiationResult() {
            ValidityStatus = ValidationResultType.Success;
            ValidityIdentifier = new Random().NextInt64();
        }
        public ValidationResultType ValidityStatus { get; }
        public long ValidityIdentifier { get; }
    }
}
