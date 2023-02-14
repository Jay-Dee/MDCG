namespace MDCG.WebApi.Data
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
