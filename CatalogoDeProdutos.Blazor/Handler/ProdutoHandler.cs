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

    public ProdutoHandler(HttpClient http)
    {
        this.http = http;
    }

    public async Task CreateProduto(CreateProdutoRequest request)
       => await http.PostAsJsonAsync("http://localhost:5064/v1/produtos/criar", request);

    public async Task ExcluirProduto(DeleteProdutoRequest request)
     => await http.DeleteAsync($"http://localhost:5064/v1/produtos/{request.id}");


    public async Task<Response<Produto?>> GetProdutoById(GetProdutoByIdRequest request)
    {
        var response = await http.GetFromJsonAsync<Produto>
        ($"http://localhost:5064/v1/produtos/{request.Id}");

        return new Response<Produto?>(response);
    }


    public async Task<Response<List<Produto>>?> GetProdutos(PagedRequest request)
    {
        string url =
        $"http://localhost:5064/v1/produtos?page={request.PageNumber}&pageSize={request.PageSize}";

        var response = await http.GetFromJsonAsync<ProdutoResponse>(url);

        return new Response<List<Produto>>(response!.Data!);

    }

    public async Task UpdateProduto(UpdateProdutoRequest request)
     => await http.PutAsJsonAsync($"http://localhost:5064/v1/produtos/{request.ProdutoId}", request);
}