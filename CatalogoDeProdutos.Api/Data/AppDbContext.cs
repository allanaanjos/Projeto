using CatalogoDeProdutos.Core.models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoDeProdutos.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
            .HasKey(x => x.Id);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Produto>().
                Property(x => x.Preco)
                .IsRequired();

            modelBuilder.Entity<Produto>()
                .Property(p => p.Descricao)
                .HasMaxLength(500);

            modelBuilder.Entity<Produto>()
                .Property(x => x.Quantidade)
                .IsRequired();

            modelBuilder.Entity<Produto>()
                .Property(x => x.Tipo)
                .IsRequired();

            modelBuilder.Entity<Produto>()
               .Property(x => x.DataDeCadastro)
               .IsRequired();

        }
    }
}