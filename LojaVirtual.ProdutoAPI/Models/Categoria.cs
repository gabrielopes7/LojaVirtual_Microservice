namespace LojaVirtual.ProdutoAPI.Models;

public class Categoria
{
    // Foi adicionado o Virtual para fazer teste de Mockagem, porém não é recomendado.
    public virtual int Id { get; set; }
    public virtual string? Nome { get; set; }
    public virtual ICollection<Produto>? Produtos { get; set; }

}
