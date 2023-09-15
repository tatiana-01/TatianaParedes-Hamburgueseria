using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class ChefRepository : GenericRepository<Chef>, IChef
{
    private readonly DbAppContext _context;
    public ChefRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Chef> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<Chef>().CountAsync();
        var registros = await _context.Set<Chef>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Include(x=>x.Hamburguesas)
            .ToListAsync();
        return (totalRegistros, registros);
    }

    public override async Task<Chef> GetByIdAsync(int id)
    {
        return await _context.Set<Chef>().Include(x=>x.Hamburguesas).FirstOrDefaultAsync(x=>x.Id==id);
    }

     public async Task<IEnumerable<Chef>> GetChefsCarnes()
    {
        var chefsCarnes= _context.Chefs.Where(p=>p.Especialidad.ToLower().Contains("carnes")).Include(p=>p.Hamburguesas);
        
        return (chefsCarnes);
    }
}
