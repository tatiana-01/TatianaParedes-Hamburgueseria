using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class CategoriaRepository : GenericRepository<Categoria>, ICategoria
{
    private readonly DbAppContext _context;
    public CategoriaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

     public override async Task<(int totalRegistros, IEnumerable<Categoria> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<Categoria>().CountAsync();
        var registros = await _context.Set<Categoria>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Include(x=>x.Hamburguesas)
            .ToListAsync();
        return (totalRegistros, registros);
    }

    public override async Task<Categoria> GetByIdAsync(int id)
    {
        return await _context.Set<Categoria>().Include(x=>x.Hamburguesas).FirstOrDefaultAsync(x=>x.Id==id);
    }

    public async Task<IEnumerable<Categoria>> GetGourmet(){
        return _context.Categorias.Where(p=>p.Descripcion.ToLower().Contains("gourmet")).Include(p=>p.Hamburguesas);
    }
}
