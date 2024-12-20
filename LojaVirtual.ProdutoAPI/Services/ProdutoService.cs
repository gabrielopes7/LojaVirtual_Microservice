using AutoMapper;
using LojaVirtual.ProdutoAPI.DTOs;
using LojaVirtual.ProdutoAPI.Models;
using LojaVirtual.ProdutoAPI.Repositories;
using LojaVirtual.ProdutoAPI.Repositories.Interfaces;
using LojaVirtual.ProdutoAPI.Services.Interfaces;

namespace LojaVirtual.ProdutoAPI.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
    {
        var produtosEntity = await _produtoRepository.GetAll();
        return _mapper.Map<IEnumerable<ProdutoDTO>>(produtosEntity);
    }

    public async Task<ProdutoDTO> GetById(int id)
    {
        var produtoEntity = await _produtoRepository.GetById(id);
        return _mapper.Map<ProdutoDTO>(produtoEntity);
    }
    public async Task AddProduto(ProdutoDTO produtoDTO)
    {
        var produtoEntity = _mapper.Map<Produto>(produtoDTO);
        await _produtoRepository.Create(produtoEntity);
        produtoDTO.Id = produtoEntity.Id;
    }
    public async Task UpdateProduto(ProdutoDTO produtoDTO)
    {
        var produtoEntity = _mapper.Map<Produto>(produtoDTO);
        await _produtoRepository.Update(produtoEntity);
        produtoDTO.Id = produtoEntity.Id;
    }

    public async Task DeleteProduto(int id)
    {
        var categoriaEntity = _produtoRepository.GetById(id).Result;
        await _produtoRepository.Delete(categoriaEntity.Id);
    }
}
