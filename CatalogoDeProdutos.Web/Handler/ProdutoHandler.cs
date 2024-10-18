using System.Net.Http.Json;
using CatalogoDeProdutos.Core.Handler;
using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.Request;
using CatalogoDeProdutos.Core.Response;


namespace CatalogoDeProdutos.Web.Handler
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
            ($"http://localhost:5064/v1/produtos/{request.id}");

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


        public async Task<List<Response<Produto>>?> GetProdutos(PagedRequest request)
        {
            string url =
            $"http://localhost:5064/v1/produtos?page={request.PageNumber}&pageSize={request.PageSize}";

            try
            {
                var produtos = await http.
                GetFromJsonAsync<List<Produto>>(url);

                var responseList = new List<Response<Produto>>();

                if (produtos != null && produtos.Any())
                {
                    responseList = produtos.Select(produto => new Response<Produto>
                    {
                        Data = produto,
                        Success = true,
                        Message = "Produto obtido com sucesso."
                    }).ToList();
                }
                else
                {
                    responseList.Add(new Response<Produto>
                    {
                        Data = null,
                        Success = false,
                        Message = "Nenhum produto encontrado."
                    });
                }

                return responseList;
            }
            catch (HttpRequestException ex)
            {
                return new List<Response<Produto>>
                {
                    new Response<Produto>
                   {
                     Data = null,
                     Success = false,
                     Message = $"Erro ao buscar os produtos: {ex.Message}"
                   }
                };
            }
        }


        public async Task<Response<Produto>> UpdateProduto(UpdateProdutoRequest request)
        {
            var response = await http
            .PutAsJsonAsync($"http://localhost:5064/v1/produtos/{request.id}", request);

            if (response.IsSuccessStatusCode)
            {
                var produtoAtualizado = await response
                .Content
                .ReadFromJsonAsync<Produto>();

                return new Response<Produto>
                {
                    Data = produtoAtualizado,
                    Success = true,
                    Message = "Produto atualizado com sucesso."
                };
            }
            else
            {
                return new Response<Produto>
                {
                    Data = null,
                    Success = false,
                    Message = "Erro ao atualizar o produto."
                };
            }
        }
    }
}