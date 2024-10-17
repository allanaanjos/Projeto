using System.ComponentModel.DataAnnotations;

namespace CatalogoDeProdutos.Core.Request
{
    public class UpdateProdutoRequest : CreateProdutoRequest
    {
        [Required(ErrorMessage = "O ID é obrigatório.")]
        public long ProdutoId { get; set; }
    }
}