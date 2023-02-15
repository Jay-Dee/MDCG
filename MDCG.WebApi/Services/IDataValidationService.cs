using MDCG.WebApi.Models;

namespace MDCG.WebApi.Services {
    public interface IDataValidationService<T> where T : class, IEntity {
        IValidationResult Validate(T entity);
    }

    public interface IValidationResult {
        ValidationResultType ValidityStatus { get; }

        long ValidityIdentifier { get; }
    }

    public enum ValidationResultType {
        Success,
        Failure,
        Unknown
    }
}
