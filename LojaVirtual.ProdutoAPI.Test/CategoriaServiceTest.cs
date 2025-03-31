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
        Mock<ICategoriaRepository>? _categoriaRepositoryMock;
        Mock<IMapper>? _mapper;
        CategoriaService? _categoriaService;

        public CategoriaServiceTest()
        {
            _categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            _mapper = new Mock<IMapper>();
            _categoriaService = new CategoriaService(_categoriaRepositoryMock.Object, _mapper.Object);
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
                   .Returns(new List<CategoriaDTO> { categoria });

            //Act
            var categoriaResultado = _categoriaService.GetCategorias().Result;

            //Assert
            Assert.NotNull(categoriaResultado);
            Assert.Single(categoriaResultado);
        }

        [Fact]
        public void DeveSerPossivelObterCategoriasComProdutos()
        {
            ICollection<ProdutoDTO> produtos = new List<ProdutoDTO>();

            //Arrange
            var categoria = CategoriaDTOConstructor.Um()
                                                   .CategoriaDTOConstructorPadrao()
                                                   .ObterMock();
            
            var produto = ProdutoDTOConstructor.Um()
                                               .ProdutoDTOConstructorPadrao()
                                               .ComId(7)
                                               .ComCategoriaId(categoria.Object.Id)
                                               .Construir();
            
            produtos.Add(produto);

            categoria.SetupGet(item => item.Produtos).Returns(produtos);

            IEnumerable<Categoria> categorias = new List<Categoria>() 
            { 
                new Categoria { 
                    Id = categoria.Object.Id,            
                    Nome = categoria.Object.Nome,                                                                    
                    Produtos = [.. produtos.Select(p => new Produto
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Preco = p.Preco,
                        Descricao = p.Descricao,
                        Estoque = p.Estoque,
                        ImagemURL = p.ImagemURL,
                        CategoriaId = p.CategoriaId
                    })]
                } 
            };

            _categoriaRepositoryMock.Setup(repo => repo.GetCategoriasProdutos())
                                    .ReturnsAsync(categorias);

            _mapper.Setup(mapper => mapper.Map<IEnumerable<CategoriaDTO>>(It.IsAny<IEnumerable<Categoria>>()))
                    .Returns(new List<CategoriaDTO> { categoria.Object });

            //Act
            var categoriaComProdutoResultado = _categoriaService.GetCategoriasProdutos().Result;

            //Assert
            Assert.NotNull(categoriaComProdutoResultado);
            Assert.Equal(categorias.First().Id, categoriaComProdutoResultado.First().Id);
            Assert.Equal(categorias.First().Nome, categoriaComProdutoResultado.First().Nome);
            Assert.Equal(categorias.First().Produtos?.Count, categoriaComProdutoResultado.First().Produtos?.Count);
            Assert.Equal(categorias.First().Produtos?.First().Id, categoriaComProdutoResultado.First().Produtos?.First().Id);
        }

        [Fact]
        public void DeveSerPossivelObterCategoriaById()
        {
            var categoria = CategoriaDTOConstructor.Um().CategoriaDTOConstructorPadrao().Construir();
            var categoriaPersistencia = new Categoria { Id = categoria.Id, Nome = categoria.Nome };

            //Arrange
            _categoriaRepositoryMock.Setup(repo => repo.GetById(categoria.Id)).ReturnsAsync(categoriaPersistencia);
            _mapper.Setup(mapper => mapper.Map<CategoriaDTO>(It.IsAny<Categoria>())).Returns(categoria);

            //Act
            var categoriaResultado = _categoriaService.GetCategoriaById(categoria.Id).Result;

            //Assert
            Assert.Equal(categoria.Id, categoriaResultado.Id);
        }

        [Fact]
        public async Task DeveSerPossivelAdicionarCategoria()
        {
            int categoriaId = 10;

            //Arrange
            var categoriaDto = CategoriaDTOConstructor.Um().CategoriaDTOConstructorPadrao().ComId(categoriaId).ObterMock();
            
            categoriaDto.SetupProperty(c => c.Id);

            var categoriaPersistida = new Categoria { Id = 20, Nome = categoriaDto.Object.Nome };

            _categoriaRepositoryMock.Setup(repo => repo.Create(It.IsAny<Categoria>()))
                                    .ReturnsAsync(categoriaPersistida);
            
            _mapper.Setup(mapper => mapper.Map<Categoria>(It.IsAny<CategoriaDTO>()))
                   .Returns(categoriaPersistida);

            //Act
            await _categoriaService.AddCategoria(categoriaDto.Object);

            //Assert
            Assert.Equal(categoriaPersistida.Id, categoriaDto.Object.Id);
        }

        [Fact]
        public async Task DeveSerPossivelAtualizarCategoria()
        {
            int categoriaId = 10;

            //Arrange
            var categoriaDto = CategoriaDTOConstructor.Um().CategoriaDTOConstructorPadrao().ComId(categoriaId).ObterMock();

            categoriaDto.SetupProperty(c => c.Id);

            var categoriaPersistida = new Categoria { Id = 34, Nome = categoriaDto.Object.Nome };

            _categoriaRepositoryMock.Setup(repo => repo.Update(It.IsAny<Categoria>()))
                                    .ReturnsAsync(categoriaPersistida);

            _mapper.Setup(mapper => mapper.Map<Categoria>(It.IsAny<CategoriaDTO>()))
                   .Returns(categoriaPersistida);
            //Act

            await _categoriaService.UpdateCategoria(categoriaDto.Object);
            //Assert

            Assert.Equal(categoriaPersistida.Id, categoriaDto.Object.Id);
        }

        [Fact]
        public async Task DeveSerPossivelRemoverCategoria()
        {
            int categoriaId = 35;
            //Arrange
            var categoriaDto = CategoriaDTOConstructor.Um().CategoriaDTOConstructorPadrao().ComId(categoriaId).ObterMock();

            var categoriaPersistidaDeletada = new Categoria { Id = categoriaId, Nome = categoriaDto.Object.Nome };

            _categoriaRepositoryMock.Setup(repo => repo.GetById(categoriaId))
                                    .ReturnsAsync(categoriaPersistidaDeletada);

            _categoriaRepositoryMock.Setup(repo => repo.Delete(categoriaId))
                                    .ReturnsAsync(categoriaPersistidaDeletada);

            //Act
            await _categoriaService.RemoveCategoria(categoriaId);

            //Assert
            Assert.True(true);
        }
        public void Dispose()
        {
            _categoriaService = null;
            _categoriaRepositoryMock = null;
            _mapper = null;
        }
    }
}