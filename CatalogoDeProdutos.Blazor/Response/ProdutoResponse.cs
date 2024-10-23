using CatalogoDeProdutos.Core.models;

namespace CatalogoDeProdutos.Blazor.Response
{
    public class ProdutoResponse
    {
        public ProdutoResponse(bool? success, List<Produto>? data, string? message, int? statusCode)
        {
            Success = success;
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }

        public bool? Success { get; set; }
        public List<Produto>? Data { get; set; } = new List<Produto>();
        public string? Message { get; set; } = string.Empty;
        public int? StatusCode { get; set; }

    }
}