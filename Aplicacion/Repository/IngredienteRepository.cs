using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class IngredienteRepository : GenericRepository<Ingrediente>, IIngrediente
{
    private readonly DbAppContext _context;
    public IngredienteRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
     public override async Task<(int totalRegistros, IEnumerable<Ingrediente> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<Ingrediente>().CountAsync();
        var registros = await _context.Set<Ingrediente>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Include(x=>x.HamburguesaIngredientes)
            .ToListAsync();
        return (totalRegistros, registros);
    }

    public override IEnumerable<Ingrediente> Find(Expression<Func<Ingrediente, bool>> expression)
    {
        return _context.Set<Ingrediente>().Where(expression).Include(p=>p.HamburguesaIngredientes);
    }

    public override async Task<Ingrediente> GetByIdAsync(int id)
    {
        return await _context.Set<Ingrediente>().Include(x=>x.HamburguesaIngredientes).FirstOrDefaultAsync(x=>x.Id==id);
    }

     public  async Task<(int totalRegistros, IEnumerable<Ingrediente> registros)> GetStock400(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<Ingrediente>().CountAsync();
        var menor400= Find(p=>p.stock<400);
        var registros =menor400
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
        return (totalRegistros, registros);
    }

     public  async Task<(int totalRegistros, IEnumerable<Ingrediente> registros)> GetPrecio2a5(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<Ingrediente>().CountAsync();
        var entre= Find(p=>p.Precio<=5 && p.Precio>=2);
        var registros =entre
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
        return (totalRegistros, registros);
    }

   public Ingrediente GetMostExpensive(){
        var Ingredientes= _context.Ingredientes.OrderByDescending(p=>p.Precio);
        return Ingredientes.First();
    } 

    public Ingrediente cambioPanPorFresco(){
        var pan= _context.Ingredientes.FirstOrDefault(x=>x.Nombre=="Pan");
       
 
            pan.Descripcion="Pan fresco y crujiente";
          
        return pan;
    }

}
