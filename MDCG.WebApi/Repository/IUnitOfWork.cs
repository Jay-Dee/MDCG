namespace MDCG.WebApi.Repository {
    public interface IUnitOfWork {
        Task CompleteAsync();
    }
}
