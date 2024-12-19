using LojaVirtual.ProdutoAPI.Models;

namespace LojaVirtual.ProdutoAPI.Repositories;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetAll();
    Task<IEnumerable<Categoria>> GetCategoriasProdutos();
    Task<Categoria> GetById(int id);
    Task<Categoria> Create(Categoria categoria);
    Task<Categoria> Update(Categoria categoria);
    Task<Categoria> Delete(int id);
}
