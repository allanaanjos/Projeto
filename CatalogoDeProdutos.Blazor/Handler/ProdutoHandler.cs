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

            if (response.IsSuccessStatusCode)
            {
                var produto = await response.Content.ReadFromJsonAsync<Produto>();
                return new Response<Produto>
                {
                    Data = produto,
                    Success = true,
                    Message = "Produto criado com sucesso."
                };
            }
            else
            {
                return new Response<Produto>
                {
                    Data = null,
                    Success = false,
                    Message = "Erro ao criar o produto."
                };
            }
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
            string url = $"http://localhost:5064/v1/produtos/{request.ProdutoId}";
            var response = new Response<Produto>();

            try
            {
                var responseHttp = await http.PutAsJsonAsync(url, request);

                if (responseHttp.IsSuccessStatusCode)
                {
                    var produtoAtualizado = await responseHttp.Content.ReadFromJsonAsync<Produto>();

                    if (produtoAtualizado != null)
                    {
                        response.Data = produtoAtualizado;
                        response.Success = true;
                        response.Message = "Produto atualizado com sucesso.";
                    }
                    else
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Message = "Falha ao atualizar o produto: dados retornados nulos.";
                    }
                }
                else
                {
                    var errorContent = await responseHttp.Content.ReadAsStringAsync();
                    response.Data = null;
                    response.Success = false;
                    response.Message = $"Falha ao atualizar o produto: {errorContent}";
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = $"Erro ao tentar atualizar o produto: {ex.Message}";
            }

            return response;
        }



    }
}