using CatalogoDeProdutos.Core.models;

namespace CatalogoDeProdutos.Blazor.Response
{
    public class ProdutoResponse
    {
    public bool Success { get; set; }
    public List<Produto> Data { get; set; } = new List<Produto>();
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }

    }
}