namespace MDCG.WebApi.Models
{
    public interface IEntity
    {
        int Id { get; set; }
        public byte[]? RowVersion { get; set; }
    }
}
