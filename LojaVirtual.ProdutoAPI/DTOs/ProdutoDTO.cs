using LojaVirtual.ProdutoAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaVirtual.ProdutoAPI.DTOs;

public class ProdutoDTO
{
    // Foi adicionado o Virtual para fazer teste de Mockagem, porém não é recomendado.
    public virtual int Id { get; set; }

    [Required(ErrorMessage = "O nome é requerido")]
    [MinLength(3)]
    [MaxLength(100)]
    public virtual string? Nome { get; set; }
    [Required(ErrorMessage = "O preço é requerido")]
    public virtual decimal Preco { get; set; }

    [Required(ErrorMessage = "A descrição é requerido")]
    [MinLength(5)]
    [MaxLength(200)]
    public virtual string? Descricao { get; set; }
    [Required(ErrorMessage = "O estoque é requerido")]
    [Range(1,9999)]
    public virtual long Estoque { get; set; }
    public virtual string? ImagemURL { get; set; }
    public string? NomeCategoria { get; set; }
    [JsonIgnore]
    public CategoriaDTO? Categoria { get; set; }
    public virtual int CategoriaId { get; set; }
}
