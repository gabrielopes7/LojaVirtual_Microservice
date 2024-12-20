using LojaVirtual.ProdutoAPI.DTOs;
using LojaVirtual.ProdutoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.ProdutoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
    {
        var produtosDto = await _produtoService.GetProdutos();

        if (produtosDto is null)
            return NotFound("Produtos não encontrados");

        return Ok(produtosDto);
    }

    [HttpGet("{id:int}", Name = "GetProduto")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get(int id)
    {
        var produtoDto = await _produtoService.GetById(id);

        if (produtoDto is null)
            return NotFound("Produto não encontrada");

        return Ok(produtoDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDTO)
    {
        if (produtoDTO is null)
            return BadRequest("Dados inválidos");

        await _produtoService.AddProduto(produtoDTO);

        return new CreatedAtRouteResult("GetProduto", new { id = produtoDTO.Id }, produtoDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Put(int id, [FromBody] ProdutoDTO produtoDTO)
    {
        if (id != produtoDTO.Id)
            return BadRequest();

        if (produtoDTO is null)
            return BadRequest();

        await _produtoService.UpdateProduto(produtoDTO);

        return Ok(produtoDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Delete(int id)
    {
        var produtoDto = await _produtoService.GetById(id);

        if (produtoDto is null)
            return NotFound("Produto não encontrado");

        await _produtoService.DeleteProduto(id);

        return Ok(produtoDto);
    }
}
