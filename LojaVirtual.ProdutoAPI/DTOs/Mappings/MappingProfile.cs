using AutoMapper;
using LojaVirtual.ProdutoAPI.Models;

namespace LojaVirtual.ProdutoAPI.DTOs.Mappings;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTO>().ReverseMap();

    }
}
