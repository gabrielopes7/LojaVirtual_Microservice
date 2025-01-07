using LojaVirtual.Web.Models;

namespace LojaVirtual.Web.Services.Contracts;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaViewModel>> GetTodasCategorias();
}
