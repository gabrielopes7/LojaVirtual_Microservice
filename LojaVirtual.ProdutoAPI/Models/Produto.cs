﻿namespace LojaVirtual.ProdutoAPI.Models;

public class Produto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public string? Descricao { get; set; }
    public long Estoque { get; set; }
    public string? ImagemURL {get; set; }

    public Categoria? Categoria { get; set; }
    public int CategoriaId { get; set; }
}
