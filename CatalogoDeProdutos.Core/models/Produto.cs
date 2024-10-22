using System;
using CatalogoDeProdutos.Core.models.Enum;

namespace CatalogoDeProdutos.Core.models
{
    public class Produto
    (
        string nome,
        double preco,
        string? descricao,
        int quantidade,
        Tipo tipo
    )
    {
        public long Id { get; set; }
        public string Nome { get; set; } = nome;
        public double Preco { get; set; } = preco;
        public string? Descricao { get; set; } = descricao;
        public int Quantidade { get; set; } = quantidade;
        public Tipo Tipo { get; set; } = tipo;
        public DateTime DataDeCadastro { get; set; } = DateTime.Now;
        public bool Inativo { get; set; } = false;


        public void Update
        (
            string nome,
            double preco,
            string? descricao, 
            int quantidade,
            Tipo tipo
         )
        {
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            Quantidade = quantidade;
            Tipo = tipo;
        }


        public void Inativar()
        {
          Inativo = true;
        }

    }
}
