using CatalogoDeProdutos.Api.Data;
using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeProdutos.Api.Repository
{
    public class Repository : IRepository<Produto>
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Produto entity)
        {
            if (entity == null)
                throw new ArgumentNullException
                (nameof(entity), "Produto não pode ser nulo.");

            try
            {
                await _context.Produtos.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception
                ("Erro ao adicionar o produto ao banco de dados.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception
                ("Erro inesperado ao adicionar o produto.", ex);
            }
        }


        public async Task DeleteAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException
                ("O ID deve ser maior que zero.", nameof(id));

            try
            {
                var produto = await _context.Produtos
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (produto == null)
                    throw new KeyNotFoundException
                    ($"Produto com ID {id} não encontrado.");

                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception
                ("Erro ao remover o produto do banco de dados.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception
                ("Erro inesperado ao remover o produto.", ex);
            }
        }


        public async Task<List<Produto>> GetAllAsync()
        {
            try
            {
                return await _context
                .Produtos
                .AsNoTracking()
                .ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception
                ("Erro ao buscar produtos do banco de dados.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception
                ("Erro inesperado ao buscar produtos.", ex);
            }
        }


        public async Task<Produto?> GetByIdAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException
                ("O ID deve ser maior que zero.", nameof(id));

            try
            {
                return await _context
                .Produtos.
                AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception
                ("Erro ao buscar o produto do banco de dados.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception
                ("Erro inesperado ao buscar o produto.", ex);
            }
        }


        public async Task<Produto> UpdateAsync(Produto entity)
        {
            if (entity == null)
                throw new ArgumentNullException
                (nameof(entity), "Produto não pode ser nulo.");

            if (entity.Id <= 0)
                throw new ArgumentException
                ("O ID deve ser maior que zero.", nameof(entity.Id));

            try
            {
                var produtoExistente = await GetByIdAsync(entity.Id);

                if (produtoExistente is null)
                    throw new KeyNotFoundException
                    ($"Produto com ID {entity.Id} não encontrado.");

                var produtoAtualizado = produtoExistente.Update
                           (entity.Nome, entity.Preco,
                             entity.Descricao, entity.Quantidade,
                             entity.Tipo);

                _context.Produtos.Update(produtoAtualizado);
                await _context.SaveChangesAsync();

                return produtoExistente;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception
                ("Erro ao atualizar o produto no banco de dados.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception
                ("Erro inesperado ao atualizar o produto.", ex);
            }
        }

    }
}