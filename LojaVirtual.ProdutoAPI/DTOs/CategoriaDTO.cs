using LojaVirtual.ProdutoAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.ProdutoAPI.DTOs;

public class CategoriaDTO
{
    // Foi adicionado o Virtual para fazer teste de Mockagem, porém não é recomendado.
    public virtual int Id { get; set; }

    [Required(ErrorMessage = "O nome é requerido")]
    [MinLength(3)]
    [MaxLength(100)]
    public virtual string? Nome { get; set; }
    public virtual ICollection<ProdutoDTO>? Produtos { get; set; }
}
