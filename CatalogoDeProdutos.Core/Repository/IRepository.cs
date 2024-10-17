using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoDeProdutos.Core.models;

namespace CatalogoDeProdutos.Core.Repository
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(long id);
        Task AddAsync(T entity);
        Task<Produto> UpdateAsync(T entity);
        Task DeleteAsync(long id);
    }
}