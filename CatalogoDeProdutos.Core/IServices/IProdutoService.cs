using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.Request;
using CatalogoDeProdutos.Core.Response;

namespace CatalogoDeProdutos.Core.Handler
{
    public interface IProdutoService
    {
        Task<Response<List<Produto>>?> GetProdutos(PagedRequest request);
        Task<Response<Produto?>> GetProdutoById(GetProdutoByIdRequest request);
        Task CreateProduto(CreateProdutoRequest request);
        Task UpdateProduto(UpdateProdutoRequest request);
        Task ExcluirProduto(DeleteProdutoRequest request);
    }
}