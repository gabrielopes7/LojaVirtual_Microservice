using LojaVirtual.ProdutoAPI.Context;
using LojaVirtual.ProdutoAPI.Models;
using LojaVirtual.ProdutoAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ProdutoAPI.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categoria>> GetAll()
    {
        return await _context.Categorias.ToListAsync();
    }
    public async Task<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        return await _context.Categorias.Include(c => c.Produtos).ToListAsync();
    }

    public async Task<Categoria> GetById(int id)
    {
        return await _context.Categorias.Where(c => c.Id == id).FirstOrDefaultAsync();
    }
    public async Task<Categoria> Create(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> Update(Categoria categoria)
    {
        _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return categoria;
    }
    public async Task<Categoria> Delete(int id)
    {
        var categoria = await GetById(id);
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

}
