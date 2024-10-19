using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.Request;
using CatalogoDeProdutos.Core.Response;

namespace CatalogoDeProdutos.Core.Handler
{
    public interface IProdutoHandler
    {
        Task<Response<List<Produto>>?> GetProdutos(PagedRequest request);
        Task<Response<Produto?>> GetProdutoById(GetProdutoByIdRequest request);
        Task<Response<Produto>> CreateProduto(CreateProdutoRequest request);
        Task<Response<Produto>> UpdateProduto(UpdateProdutoRequest request);
        Task<Response<Produto>> DeleteProduto(DeleteProdutoRequest request);
    }
}