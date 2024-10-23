using CatalogoDeProdutos.Core.Handler;
using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.Repository;
using CatalogoDeProdutos.Core.Request;
using CatalogoDeProdutos.Core.Response;

public class ProdutoService : IProdutoService
{
    private readonly IGenericRepository<Produto> _repository;

    public ProdutoService(IGenericRepository<Produto> repository)
    {
        _repository = repository ??
        throw new ArgumentNullException(nameof(repository));
    }

    public async Task CreateProduto(CreateProdutoRequest request)
    {
        var novoProduto = new Produto(
            request.Nome,
            request.Preco,
            request.Descricao,
            request.Quantidade,
            tipo: request.Tipo
        );

        await _repository.AddAsync(novoProduto);
        await _repository.SaveChanges();
    }

    public async Task ExcluirProduto(DeleteProdutoRequest request)
    {
        var produto = await _repository.GetByIdAsync(request.Id);

        if (produto is null)
            throw new KeyNotFoundException
            ($"Produto com ID {request.Id} não foi encontrado.");

        produto.Inativar();
        await _repository.SaveChanges();
    }

    public async Task<Response<Produto?>> GetProdutoById(GetProdutoByIdRequest request)
    {
        var produto = await _repository.GetByIdAsync(request.Id);

        if (produto == null)
        {
            return new Response<Produto?>
            (
                data: null,
                message: $"Produto com ID {request.Id} não encontrado."
            );
        }

        return new Response<Produto?>(data: produto);
    }

    public async Task<Response<List<Produto>>?> GetProdutos(PagedRequest request)
    {

        var produtos = await _repository.GetAllAsync();

        return new Response<List<Produto>>(data: produtos!
              .Skip((request.PageNumber - 1) * request.PageSize)
              .Take(request.PageSize)
              .ToList(),
              message: "Produtos obtidos com sucesso.");


    }


    public async Task UpdateProduto(UpdateProdutoRequest request)
    {

        var produtoExistente = await _repository.GetByIdAsync(request.ProdutoId);

        if (produtoExistente == null)
            throw new KeyNotFoundException
            ($"Produto com ID {request.ProdutoId} não foi encontrado.");

        produtoExistente.Update(
            request.Nome,
            request.Preco,
            request.Descricao,
            request.Quantidade,
            request.Tipo
        );

        await _repository.SaveChanges();
    }


}
