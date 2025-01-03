using LojaVirtual.Web.Models;

namespace LojaVirtual.Web.Services.Contracts;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoViewModel>> GetTodosProdutos();
    Task<ProdutoViewModel> ProcurarProdutoById(int id);
    Task<ProdutoViewModel> CreateProduto(ProdutoViewModel produtoViewModel);
    Task<ProdutoViewModel> UpdateProduto(ProdutoViewModel produtoViewModel);
    Task<bool> DeleteProdutoById(int id);
}
