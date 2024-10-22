using CatalogoDeProdutos.Core.models;
using CatalogoDeProdutos.Core.models.Enum;
using CatalogoDeProdutos.Core.Repository;
using CatalogoDeProdutos.Core.Request;
using Moq;

namespace CatalogoDeProdutos.Teste
{
    public class ProdutoServiceTeste
    {
        private readonly Mock<IGenericRepository<Produto>> _repositoryMock;
        private readonly ProdutoService _service;

        public ProdutoServiceTeste()
        {
            _repositoryMock = new Mock<IGenericRepository<Produto>>();
            _service = new ProdutoService(_repositoryMock.Object);
        }


        [Fact]
        public async Task CriarProduto_DeveAdicionarProduto_QuandoRequisicaoValida()
        {

            var request = new CreateProdutoRequest
            {
                Nome = "Produto Teste",
                Preco = 10.0,
                Descricao = "Descrição do Produto",
                Quantidade = 5,
                Tipo = Tipo.Organico
            };

            await _service.CreateProduto(request);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Produto>()), Times.Once);
            _repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
        }


        [Fact]
        public async Task ExcluirProduto_DeveInativarProduto_QuandoExiste()
        {
            var produto = new Produto("Produto Teste", 10.0, "Descrição", 5, Tipo.Organico)
            {
                Id = 1
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produto);

            var request = new DeleteProdutoRequest { Id = 1 };

            await _service.ExcluirProduto(request);

            Assert.True(produto.Inativo);
            _repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task ObterProdutoPorId_DeveRetornarProduto_QuandoExiste()
        {
            var produto = new Produto("Produto Teste", 10.0, "Descrição", 5, Tipo.Organico)
            {
                Id = 1
            };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produto);

            var request = new GetProdutoByIdRequest { Id = 1 };

            var response = await _service.GetProdutoById(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(produto, response.Data);
        }

        [Fact]
        public async Task ObterProdutos_DeveRetornarListaPaginated_QuandoChamado()
        {
            var produtos = new List<Produto>
        {
            new Produto("Produto 1", 10.0, "Descrição 1", 5, Tipo.Organico),
            new Produto("Produto 2", 15.0, "Descrição 2", 10, Tipo.NaoOrganico)
        };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(produtos);

            var request = new PagedRequest { PageNumber = 1, PageSize = 1 };

            var response = await _service.GetProdutos(request);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Single(response.Data); 
        }

        [Fact]
        public async Task AtualizarProduto_DeveAtualizar_QuandoProdutoExiste()
        {
            var produtoExistente = new Produto("Produto Teste", 10.0, "Descrição", 5, Tipo.Organico)
            {
                Id = 1
            };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(produtoExistente);

            var request = new UpdateProdutoRequest
            {
                ProdutoId = 1,
                Nome = "Produto Atualizado",
                Preco = 12.0,
                Descricao = "Nova Descrição",
                Quantidade = 8,
                Tipo = Tipo.NaoOrganico
            };

            await _service.UpdateProduto(request);

            Assert.Equal("Produto Atualizado", produtoExistente.Nome);
            Assert.Equal(12.0, produtoExistente.Preco);
            _repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
        }
    }
}