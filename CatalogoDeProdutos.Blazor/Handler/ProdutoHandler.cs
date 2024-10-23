using System.Net.Http.Json;
using CatalogoDeProdutos.Blazor.Response;
using CatalogoDeProdutos.Core.Handler;
using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.Request;
using CatalogoDeProdutos.Core.Response;

namespace CatalogoDeProdutos.Blazor.Handler;

public class ProdutoHandler : IProdutoService
{
    private readonly HttpClient http;

    private string _baseUrl = "http://localhost:5064/v1/produtos";

    public ProdutoHandler(HttpClient http)
    {
        this.http = http;
    }

    public async Task CreateProduto(CreateProdutoRequest request)
       => await http.PostAsJsonAsync($"{_baseUrl}/criar", request);

    public async Task ExcluirProduto(DeleteProdutoRequest request)
     => await http.DeleteAsync($"{_baseUrl}/{request.id}");


    public async Task<Response<Produto?>> GetProdutoById(GetProdutoByIdRequest request)
    {
        var response = await http.GetFromJsonAsync<Produto>
        ($"{_baseUrl}/{request.Id}");

        return new Response<Produto?>(response);
    }


    public async Task<Response<List<Produto>>?> GetProdutos(PagedRequest request)
    {
        string url =
        $"{_baseUrl}?page={request.PageNumber}&pageSize={request.PageSize}";

        var response = await http.GetFromJsonAsync<ProdutoResponse>(url);

        return new Response<List<Produto>>(response!.Data!);

    }

    public async Task UpdateProduto(UpdateProdutoRequest request)
     => await http.PutAsJsonAsync($"{_baseUrl}/{request.ProdutoId}", request);
}