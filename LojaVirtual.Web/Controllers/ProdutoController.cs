using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
        {
            var result = await _produtoService.GetTodosProdutos();

            if (result is null)
                return View("Error");

            return View(result);
        }
    }
}
