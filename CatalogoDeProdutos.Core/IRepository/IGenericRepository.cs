namespace CatalogoDeProdutos.Core.Repository
{
    public interface IGenericRepository<T>
    {
        Task<List<T>?> GetAllAsync();
        Task<T?> GetByIdAsync(long id);
        Task AddAsync(T entity);
        Task SaveChanges();
    }
}