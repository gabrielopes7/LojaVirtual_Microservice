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

        [HttpGet]

        public async Task<ActionResult> UpdateProduto(int id)
        {
            ViewBag.CategoriaId = new SelectList(await
                _categoriaService.GetTodasCategorias(), "Id", "Nome");
            
            var result = await _produtoService.ProcurarProdutoById(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProduto(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _produtoService.UpdateProduto(produtoViewModel);

                if (result is not null)
                    return RedirectToAction(nameof(Index));
            }

            return View(produtoViewModel);
        }

        [HttpGet]
        public async Task<ActionResult<ProdutoViewModel>> DeleteProduto(int id)
        {
            var result = await _produtoService.ProcurarProdutoById(id);

            if(result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost, ActionName("DeleteProduto")]
        public async Task<ActionResult> DeleteProdutoConfirmado(int id)
        {
            var result = await _produtoService.DeleteProdutoById(id);

            if(!result)
                return View("Error");

            return RedirectToAction(nameof(Index));
        }
    }
}
