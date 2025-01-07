using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Web.Models;

public class ProdutoViewModel
{
    public int Id { get; set; }
    [Required]
    public string? Nome { get; set; }
    [Required]
    [Display(Name = "Preço")]
    public decimal Preco { get; set; }
    [Required]
    [Display(Name = "Descrição")]
    public string? Descricao { get; set; }
    [Required]
    public long Estoque { get; set; }
    [Required]
    public string? ImagemURL { get; set; }
    public string? NomeCategoria { get; set; }
    [Display(Name = "Categorias")]
    public int CategoriaId { get; set; }
}
