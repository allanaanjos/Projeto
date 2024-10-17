using System.ComponentModel.DataAnnotations;
using CatalogoDeProdutos.Core.models.Enum;

namespace CatalogoDeProdutos.Core.Request
{
    public class CreateProdutoRequest : Request
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "O preço não pode ser negativo.")]
        public double Preco { get; set; }

        [MaxLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string? Descricao { get; set; } = null;

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório.")]
        public Tipo Tipo { get; set; }
    }
}
