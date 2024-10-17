using System.ComponentModel.DataAnnotations;

namespace CatalogoDeProdutos.Core.Request
{
    public class DeleteProdutoRequest : Request
    {
        [Required(ErrorMessage = "O ID é obrigatório.")]
        public long Id { get; set; }
    }
}