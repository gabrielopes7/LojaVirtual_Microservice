namespace LojaVirtual.ProdutoAPI.Models;

public class Produto
{  
    // Foi adicionado o Virtual para fazer teste de Mockagem, porém não é recomendado.
    public virtual int Id { get; set; }
    public virtual string? Nome { get; set; }
    public virtual decimal Preco { get; set; }
    public virtual string? Descricao { get; set; }
    public virtual long Estoque { get; set; }
    public virtual string? ImagemURL {get; set; }

    public Categoria? Categoria { get; set; }
    public virtual int CategoriaId { get; set; }
}
