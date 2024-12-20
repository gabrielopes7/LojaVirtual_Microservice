using LojaVirtual.ProdutoAPI.DTOs;

namespace LojaVirtual.ProdutoAPI.Services.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaDTO>> GetCategorias();
    Task<IEnumerable<CategoriaDTO>> GetCategoriasProdutos();
    Task<CategoriaDTO> GetCategoriaById(int id);
    Task AddCategoria(CategoriaDTO categoriaDTO);
    Task UpdateCategoria(CategoriaDTO categoriaDTO);
    Task RemoveCategoria(int id);
}
