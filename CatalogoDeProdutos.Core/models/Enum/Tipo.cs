using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CatalogoDeProdutos.Core.models.Enum
{
    public enum Tipo
    {
        [Display(Name = "Orgânico")]
        Organico = 1, 
        
        [Display(Name = "Não Orgânico")]
        NaoOrganico = 2
    }
}