using LojaVirtual.ProdutoAPI.DTOs;

namespace LojaVirtual.ProdutoAPI.Services.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoDTO>> GetProdutos();
    Task<ProdutoDTO> GetById(int id);
    Task AddProduto(ProdutoDTO produtoDTO);
    Task UpdateProduto(ProdutoDTO produtoDTO);
    Task DeleteProduto(int id);
}
