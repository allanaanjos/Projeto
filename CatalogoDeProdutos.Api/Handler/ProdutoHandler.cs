using CatalogoDeProdutos.Core.Handler;
using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.Repository;
using CatalogoDeProdutos.Core.Request;
using CatalogoDeProdutos.Core.Response;

public class ProdutoHandler : IProdutoHandler
{
    private readonly IRepository<Produto> _repository;

    public ProdutoHandler(IRepository<Produto> repository)
    {
        _repository = repository ??
        throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Response<Produto>> CreateProduto(CreateProdutoRequest request)
    {
        try
        {
            var novoProduto = new Produto(
                request.Nome,
                request.Preco,
                request.Descricao,
                request.Quantidade,
                tipo: request.Tipo
            );

            await _repository.AddAsync(novoProduto);

            return new Response<Produto>(novoProduto, "Produto Cadastrado!", 200);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao criar o produto", ex);
        }
    }

    public async Task<Response<Produto>> DeleteProduto(DeleteProdutoRequest request)
    {
        try
        {
            var produtoExistente = await _repository.GetByIdAsync(request.Id);

            if (produtoExistente == null)
                return new Response<Produto>(
                    message: $"Produto com ID {request.Id} não encontrado.",
                    statusCode: StatusCodes.Status404NotFound);

            await _repository.DeleteAsync(request.Id);

            return new Response<Produto>(
                data: produtoExistente,
                message: "Produto removido com sucesso!",
                statusCode: StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return new Response<Produto>(
                message: $"Erro ao deletar o produto: {ex.Message}",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<Response<Produto?>> GetProdutoById(GetProdutoByIdRequest request)
    {
        try
        {
            var produto = await _repository.GetByIdAsync(request.Id);

            if (produto == null)
            {
                return new Response<Produto?>(
                    data: null,
                    message: $"Produto com ID {request.Id} não encontrado.",
                    statusCode: StatusCodes.Status404NotFound);
            }

            return new Response<Produto?>(
                data: produto,
                statusCode: StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return new Response<Produto?>(
                data: null,
                message: $"Erro ao buscar o produto por ID: {ex.Message}",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<Response<List<Produto>>?> GetProdutos(PagedRequest request)
    {
        try
        {
            var produtos = await _repository.GetAllAsync();

            if (produtos == null || !produtos.Any())
            {
                return new Response<List<Produto>>(
                    data: null,
                    message: "Nenhum produto cadastrado.",
                    statusCode: StatusCodes.Status404NotFound
                );
            }

            return new Response<List<Produto>>(
                data:produtos.Skip((request.PageNumber - 1) * request.PageSize)
                  .Take(request.PageSize)
                  .ToList(),
                message: "Produtos obtidos com sucesso.",
                statusCode: StatusCodes.Status200OK
            );
        }
        catch (Exception ex)
        {
            return new Response<List<Produto>>(
                data: null,
                message: $"Erro ao buscar os produtos: {ex.Message}",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }


    public async Task<Response<Produto>> UpdateProduto(UpdateProdutoRequest request)
    {
        try
        {
            var produtoExistente = await _repository.GetByIdAsync(request.ProdutoId);

            if (produtoExistente == null)
                return new Response<Produto>
                ($"Produto com ID {request.ProdutoId} não encontrado.",
                StatusCodes.Status404NotFound);

            produtoExistente.Update(
                request.Nome,
                request.Preco,
                request.Descricao,
                request.Quantidade,
                request.Tipo
            );

            await _repository.UpdateAsync(produtoExistente);

            return new Response<Produto>(produtoExistente,
            "Produto atualizado com sucesso!",
            StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return new Response<Produto>
            ($"Erro ao atualizar o produto, {ex.Message}",
             StatusCodes.Status500InternalServerError);
        }
    }
}
