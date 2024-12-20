using LojaVirtual.ProdutoAPI.DTOs;
using LojaVirtual.ProdutoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.ProdutoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
    {
        var categoriasDto = await _categoriaService.GetCategorias();
        
        if (categoriasDto is null)
            return NotFound("Categorias não encontradas");
        
        return Ok(categoriasDto);
    }

    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasProdutos()
    {
        var categoriasDto = await _categoriaService.GetCategoriasProdutos();

        if (categoriasDto is null)
            return NotFound("Categoria não encontrada");

        return Ok(categoriasDto);
    }

    [HttpGet("{id:int}", Name = "GetCategoria")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get(int id)
    {
        var categoriasDto = await _categoriaService.GetCategoriaById(id);

        if (categoriasDto is null)
            return NotFound("Categoria não encontradas");

        return Ok(categoriasDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null)
            return BadRequest("Dados inválidos");

        await _categoriaService.AddCategoria(categoriaDTO);

        return new CreatedAtRouteResult("GetCategoria", new { id = categoriaDTO.Id} , categoriaDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Put(int id, [FromBody] CategoriaDTO categoriaDTO)
    {
        if (id != categoriaDTO.Id)
            return BadRequest();

        if (categoriaDTO is null)
            return BadRequest();

        await _categoriaService.UpdateCategoria(categoriaDTO);

        return Ok(categoriaDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Delete(int id)
    {
        var categoriaDto = await _categoriaService.GetCategoriaById(id);

        if(categoriaDto is null)
            return NotFound("Categoria não encontrada");

        await _categoriaService.RemoveCategoria(id);

        return Ok(categoriaDto);
    }
}

