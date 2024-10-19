using System.Net.Http.Json;
using CatalogoDeProdutos.Blazor.Response;
using CatalogoDeProdutos.Core.Handler;
using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.Request;
using CatalogoDeProdutos.Core.Response;

namespace CatalogoDeProdutos.Blazor.Handler
{
    public class ProdutoHandler : IProdutoHandler
    {
        private readonly HttpClient http;

        public ProdutoHandler(HttpClient http)
        {
            this.http = http;
        }

        public async Task<Response<Produto>> CreateProduto(CreateProdutoRequest request)
        {
            var response = await http.PostAsJsonAsync
            ("http://localhost:5064/v1/produtos/criar", request);

            return await response.Content.ReadFromJsonAsync<Response<Produto>>()
            ?? new Response<Produto>(null!, "Produto não Cadastrado", 400);

        }
        


        public async Task<Response<Produto>> DeleteProduto(DeleteProdutoRequest request)
        {
            var response = await http.DeleteAsync
            ($"http://localhost:5064/v1/produtos/{request.id}");

            if (response.IsSuccessStatusCode)
            {
                return new Response<Produto>
                {
                    Data = null,
                    Success = true,
                    Message = "Produto deletado com sucesso."
                };
            }
            else
            {
                return new Response<Produto>
                {
                    Data = null,
                    Success = false,
                    Message = "Erro ao deletar o produto."
                };
            }
        }


        public async Task<Response<Produto?>> GetProdutoById(GetProdutoByIdRequest request)
        {
            var produto = await http.GetFromJsonAsync<Produto>
            ($"http://localhost:5064/v1/produtos/{request.Id}");

            if (produto != null)
            {
                return new Response<Produto?>
                {
                    Data = produto,
                    Success = true,
                    Message = "Produto encontrado com sucesso."
                };
            }
            else
            {
                return new Response<Produto?>
                {
                    Data = null,
                    Success = false,
                    Message = "Produto não encontrado."
                };
            }
        }


        public async Task<Response<List<Produto>>?> GetProdutos(PagedRequest request)
        {
            try
            {
                string url =
                $"http://localhost:5064/v1/produtos?page={request.PageNumber}&pageSize={request.PageSize}";

                var response = await http.GetFromJsonAsync<ProdutoResponse>(url);

                if (response == null || !response.Success || response.Data == null || !response.Data.Any())
                {
                    return new Response<List<Produto>>
                    {
                        Success = false,
                        Message = response?.Message ?? "Nenhum produto cadastrado.",
                    };
                }

                return new Response<List<Produto>>
                {
                    Success = true,
                    Data = response.Data,
                    Message = "Produtos obtidos com sucesso.",
                };
            }
            catch (Exception ex)
            {
                return new Response<List<Produto>>
                {
                    Success = false,
                    Message = $"Erro ao buscar os produtos: {ex.Message}",
                };
            }
        }

        public async Task<Response<Produto>> UpdateProduto(UpdateProdutoRequest request)
        {
            var result = await http.PutAsJsonAsync
            ($"http://localhost:5064/v1/produtos/{request.ProdutoId}", request);

            return await result.Content.ReadFromJsonAsync<Response<Produto>>()
             ?? new Response<Produto>(null!, "Não Possivel atualizar produto.", 400);
        }


    }
}