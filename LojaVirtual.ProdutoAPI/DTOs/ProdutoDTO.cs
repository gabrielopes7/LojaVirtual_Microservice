using LojaVirtual.ProdutoAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaVirtual.ProdutoAPI.DTOs;

public class ProdutoDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é requerido")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "O preço é requerido")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "A descrição é requerido")]
    [MinLength(5)]
    [MaxLength(200)]
    public string? Descricao { get; set; }
    [Required(ErrorMessage = "O estoque é requerido")]
    [Range(1,9999)]
    public long Estoque { get; set; }
    public string? ImagemURL { get; set; }
    public string? NomeCategoria { get; set; }
    [JsonIgnore]
    public Categoria? Categoria { get; set; }
    public int CategoriaId { get; set; }
}
