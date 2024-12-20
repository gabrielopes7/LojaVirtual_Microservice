
using AutoMapper;
using LojaVirtual.ProdutoAPI.DTOs;
using LojaVirtual.ProdutoAPI.Models;
using LojaVirtual.ProdutoAPI.Repositories.Interfaces;
using LojaVirtual.ProdutoAPI.Services.Interfaces;

namespace LojaVirtual.ProdutoAPI.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;
    public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
    {
        var categoriasEntity = await _categoriaRepository.GetAll();
        return _mapper.Map<IEnumerable<CategoriaDTO>>(categoriasEntity);
    }
    public async Task<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
    {
        var categoriasEntity = await _categoriaRepository.GetCategoriasProdutos();
        return _mapper.Map<IEnumerable<CategoriaDTO>>(categoriasEntity);
    }
    public async Task<CategoriaDTO> GetCategoriaById(int id)
    {
        var categoriaEntity = await _categoriaRepository.GetById(id);
        return _mapper.Map<CategoriaDTO>(categoriaEntity);
    }
    
    public async Task AddCategoria(CategoriaDTO categoriaDTO)
    {
        var categoriaEntity = _mapper.Map<Categoria>(categoriaDTO);
        await _categoriaRepository.Create(categoriaEntity);
        categoriaDTO.Id = categoriaEntity.Id;
    }
    public async Task UpdateCategoria(CategoriaDTO categoriaDTO)
    {
        var categoriaEntity = _mapper.Map<Categoria>(categoriaDTO);
        await _categoriaRepository.Update(categoriaEntity);
        categoriaDTO.Id = categoriaEntity.Id;
    }
    public async Task RemoveCategoria(int id)
    {
        var categoriaEntity = _categoriaRepository.GetById(id).Result;
        await _categoriaRepository.Delete(categoriaEntity.Id);
    }
}
