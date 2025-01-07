using AutoMapper;
using LojaVirtual.ProdutoAPI.Models;

namespace LojaVirtual.ProdutoAPI.DTOs.Mappings;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<ProdutoDTO, Produto>();
        CreateMap<Produto, ProdutoDTO>()
            .ForMember(c => c.NomeCategoria, opt => opt.MapFrom(src => src.Categoria.Nome));

    }
}
