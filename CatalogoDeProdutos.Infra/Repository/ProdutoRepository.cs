using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.Repository;
using CatalogoDeProdutos.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeProdutos.Infra.Repository
{
    public class ProdutoRepository : IGenericRepository<Produto>
    {
        private readonly AppDbContext context;
        public ProdutoRepository(AppDbContext dbContext) => context = dbContext;

        public async Task AddAsync(Produto entity) => await context.AddAsync(entity);

        public async Task<List<Produto>?> GetAllAsync() =>
            await context.Produtos
                         .AsNoTracking()
                         .Where(x => !x.Inativo)
                         .ToListAsync();

        public async Task<Produto?> GetByIdAsync(long id) => await context.Produtos.FindAsync(id);

        public Task SaveChanges() => context.SaveChangesAsync();


    }
}