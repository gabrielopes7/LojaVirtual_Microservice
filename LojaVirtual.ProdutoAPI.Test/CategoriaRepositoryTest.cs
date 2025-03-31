using LojaVirtual.ProdutoAPI.Context;
using LojaVirtual.ProdutoAPI.Models;
using LojaVirtual.ProdutoAPI.Repositories;
using LojaVirtual.ProdutoAPI.Test.Constructor;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaVirtual.ProdutoAPI.Test
{
    public class CategoriaRepositoryTest : IDisposable
    {
        CategoriaRepository? _categoriaRepository;

        public CategoriaRepositoryTest() 
        {
            _categoriaRepository = new CategoriaRepository(GetDbContext());
        }

        private AppDbContext GetDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            var dbContext = new AppDbContext(options);

            dbContext.Database.EnsureCreated();

            dbContext.SaveChanges();
            return dbContext;
        }

        [Fact]
        public async Task ObterTodasCategoriasTest()
        {
            //Arrange
            var dbContext = GetDbContext();

            //Act

            var categorias = await _categoriaRepository.GetAll();

            //Assert

            Assert.NotNull(categorias);
            Assert.Equal(2, categorias.Count());
        }


        [Fact]
        public async Task ObterTodasCategoriaComProdutosTest()
        {
            //Arrange
            var dbContext = GetDbContext();
            //Act
            var categorias = await _categoriaRepository.GetCategoriasProdutos();

            //Assert
            Assert.NotNull(categorias);
            Assert.NotEmpty(categorias);
            Assert.NotEmpty(categorias.Select(cat => cat.Produtos));
        }


        [Fact]
        public async Task ObterCategoriaPorIdTest()
        {
            int categoriaId = 1;
            //Arrange
            var dbContext = GetDbContext();
            //Act
            var categoria = await _categoriaRepository.GetById(categoriaId);
            //Assert

            Assert.NotNull(categoria);
            Assert.Equal(categoriaId, categoria.Id);
        }


        [Fact]
        public async Task AdicionarCategoriaTest()
        {
            //Arrange
            var categoriaAdd = new Categoria { Id = 3, Nome = "Cadernos Variados" };
            var dbContext = GetDbContext();
            
            //Act
            var categoria = await _categoriaRepository.Create(categoriaAdd);

            //Assert

            Assert.NotNull(categoria);
            Assert.Equal(categoriaAdd.Id, categoria.Id);
            Assert.Equal(categoriaAdd.Nome, categoria.Nome);
        }


        [Fact]
        public async Task AtualizarCategoriaTest()
        {
            int categoriaId = 1;
            string categoriaNome = "Artigos atualizados";
            //Arrange
            var dbContext = GetDbContext();
            var categoriaAtt = new Categoria { Id = categoriaId, Nome = categoriaNome };

            //Act
            var categoria = await _categoriaRepository.Update(categoriaAtt);

            //Assert

            Assert.NotNull(categoria);
            Assert.Equal(categoriaAtt.Id, categoria.Id);
            Assert.Equal(categoriaAtt.Nome, categoria.Nome);
        }


        [Fact]
        public async Task DeletarCategoriaTest()
        {
            int categoriaId = 2;

            //Arrange
            var categoriaDel = new Categoria { Id = categoriaId, Nome = "Cadernos Variados" };
            var dbContext = GetDbContext();

            //Act
            var categoria = await _categoriaRepository.Delete(categoriaDel.Id);

            //Assert

            Assert.NotNull(categoria);
            Assert.Equal(categoriaDel.Id, categoria.Id);
        }
        public void Dispose()
        {
            _categoriaRepository = null;
        }
    }
}
