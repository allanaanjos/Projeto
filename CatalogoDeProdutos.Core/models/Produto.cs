using System;
using CatalogoDeProdutos.Core.models.Enum;

namespace CatalogoDeProdutos.Core.models
{
    public class Produto
    {
        public Produto(string nome, double preco,
        string? descricao, int quantidade,
        Tipo tipo)
        {
            Validacao(nome, preco, descricao, quantidade, tipo);
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            Quantidade = quantidade;
            Tipo = tipo;
            DataDeCadastro = DateTime.Now;
        }

        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public double Preco { get; set; }
        public string? Descricao { get; set; } = null;
        public int Quantidade { get; set; }
        public Tipo Tipo { get; set; } 
        public DateTime DataDeCadastro { get; set; } 

        private void Validacao(string nome, double preco,
        string? descricao, int quantidade,
         Tipo tipo)
        {
            if (string.IsNullOrEmpty(nome) || nome.Length < 3)
                throw new ArgumentException
                ("Nome não pode ser menor que 3 caracteres", nameof(nome));

            if (preco < 0)
                throw new ArgumentException
                ("Preço não pode ser negativo", nameof(preco));

            if (descricao != null && descricao.Length > 500)
                throw new ArgumentException
                ("Descrição não pode exceder 500 caracteres", nameof(descricao));

            if (quantidade < 0)
                throw new ArgumentException
                ("Quantidade não pode ser negativa", nameof(quantidade));

            if (tipo != Tipo.Organico && tipo != Tipo.NaoOrganico)
                throw new ArgumentException
                ("Tipo inválido", nameof(tipo));
        }

        public Produto Update(string nome, double preco,
         string? descricao, int quantidade, Tipo tipo)
        {
            Validacao(nome, preco, descricao, quantidade, tipo)  ;
            this.Nome = nome;
            this.Preco = preco;
            this.Descricao = descricao;
            this.Quantidade = quantidade;
            this.Tipo = tipo;
            return this;
        }

    }
}
