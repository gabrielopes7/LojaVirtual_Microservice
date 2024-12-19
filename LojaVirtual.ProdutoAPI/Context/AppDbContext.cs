using LojaVirtual.ProdutoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ProdutoAPI.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Categoria
        modelBuilder.Entity<Categoria>()
            .HasKey(c => c.Id);
        
        modelBuilder.Entity<Categoria>()
            .Property(c => c.Nome)
            .HasMaxLength(100)
            .IsRequired();

        //Produto
        modelBuilder.Entity<Produto>()
            .Property(c => c.Nome)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Produto>()
            .Property(c => c.Descricao)
            .HasMaxLength(255)
            .IsRequired();
        modelBuilder.Entity<Produto>()
           .Property(c => c.ImagemURL)
           .HasMaxLength(255)
           .IsRequired();

        modelBuilder.Entity<Produto>()
           .Property(c => c.Preco)
           .HasPrecision(12, 2);

        modelBuilder.Entity<Categoria>()
            .HasMany(g => g.Produtos)
            .WithOne(c => c.Categoria)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Categoria>().HasData(
            new Categoria
            {
                Id = 1,
                Nome = "Material Escolar",
            },
            new Categoria
            {
                Id = 2,
                Nome = "Acessórios"
            }
        );

        modelBuilder.Entity<Produto>().HasData(
            new Produto
            {
                Id = 1,
                Nome = "Caderno",
                Descricao = "Caderno de espiral",
                Preco = 9.80M,
                ImagemURL = "caderno1.jpg",
                CategoriaId = 1
            },
            new Produto
            {
                Id = 2,
                Nome = "Lápis",
                Descricao = "Lápis na cor preta",
                Preco = 3.75M,
                ImagemURL = "lapis1.jpg",
                CategoriaId = 1
            },
            new Produto
            {
                Id = 3,
                Nome = "Clips",
                Descricao = "Clips para papel",
                Preco = 1.99M,
                ImagemURL = "clips1.jpg",
                CategoriaId = 2
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
