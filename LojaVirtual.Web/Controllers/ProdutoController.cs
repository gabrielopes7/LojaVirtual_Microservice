using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaVirtual.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;

        public ProdutoController(IProdutoService produtoService, ICategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
        {
            var result = await _produtoService.GetTodosProdutos();

            if (result is null)
                return View("Error");

            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> CreateProduto()
        {
            ViewBag.CategoriaId = new SelectList(await
                _categoriaService.GetTodasCategorias(), "Id", "Nome");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduto(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _produtoService.CreateProduto(produtoViewModel);

                if (result is not null)
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(await
               _categoriaService.GetTodasCategorias(), "Id", "Nome");
            }

            return View(produtoViewModel);
        }
    }
}
