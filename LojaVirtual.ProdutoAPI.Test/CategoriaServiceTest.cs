using AutoMapper;
using LojaVirtual.ProdutoAPI.DTOs;
using LojaVirtual.ProdutoAPI.Models;
using LojaVirtual.ProdutoAPI.Repositories;
using LojaVirtual.ProdutoAPI.Repositories.Interfaces;
using LojaVirtual.ProdutoAPI.Services;
using LojaVirtual.ProdutoAPI.Services.Interfaces;
using LojaVirtual.ProdutoAPI.Test.Constructor;
using Moq;

namespace LojaVirtual.ProdutoAPI.Test
{
    public class CategoriaServiceTest : IDisposable
    {
        Mock<ICategoriaRepository> _categoriaRepositoryMock;
        Mock<IMapper> _mapper;
        CategoriaService _categoriaService;

        public CategoriaServiceTest()
        {
            _categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            _mapper = new Mock<IMapper>();
            _categoriaService = new CategoriaService(_categoriaRepositoryMock.Object, _mapper.Object);
        }

        [Fact]
        public void DeveSerPossivelObterCategoriaById()
        {
            var categoria = CategoriaDTOConstructor.Um().CategoriaDTOConstructorPadrao().Construir();

            
            //Arrange
            _categoriaRepositoryMock.Setup(repo => repo.GetById(categoria.Id)).Returns(Task.FromResult(new Categoria { Id = categoria.Id, Nome = categoria.Nome }));
            _mapper.Setup(mapper => mapper.Map<CategoriaDTO>(It.IsAny<Categoria>())).Returns(new CategoriaDTO { Id = categoria.Id, Nome = categoria.Nome });

            //Act
            var categoriaResultado = _categoriaService.GetCategoriaById(categoria.Id).Result;

            //Assert

            Assert.Equal(categoria.Id, categoriaResultado.Id);
        }

        [Fact]

        public void DeveSerPossivelObterTodasCategorias()
        {
            var categoria = CategoriaDTOConstructor.Um().CategoriaDTOConstructorPadrao().Construir();
            IEnumerable<Categoria> categorias = new List<Categoria>() { new Categoria { Id = categoria.Id, Nome = categoria.Nome } };

            //Arrange
            _categoriaRepositoryMock.Setup(repo => repo.GetAll())
                                    .Returns(Task.FromResult(categorias));

            _mapper.Setup(mapper => mapper.Map<IEnumerable<CategoriaDTO>>(It.IsAny<IEnumerable<Categoria>>()))
                   .Returns(new List<CategoriaDTO> { new CategoriaDTO { Id = categoria.Id, Nome = categoria.Nome } });

            //Act
            var categoriaResultado = _categoriaService.GetCategorias().Result;

            //Assert
            Assert.NotNull(categoriaResultado);
            Assert.Single(categoriaResultado);
        }

        public void Dispose()
        {
            _categoriaService = null;
            _categoriaRepositoryMock = null;
        }
    }
}