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
public class HamburguesaIngredienteRepository: IHamburguesaIngrediente
{
    private readonly DbAppContext _context;

    public HamburguesaIngredienteRepository(DbAppContext context)
    {
        _context = context;
    }

    public virtual void Add(HamburguesaIngrediente entity)
    {
        _context.Set<HamburguesaIngrediente>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<HamburguesaIngrediente> entities)
    {
        _context.Set<HamburguesaIngrediente>().AddRange(entities);
    }

    public virtual IEnumerable<HamburguesaIngrediente> Find(Expression<Func<HamburguesaIngrediente, bool>> expression)
    {
        return _context.Set<HamburguesaIngrediente>().Where(expression);
    }

    public virtual async Task<IEnumerable<HamburguesaIngrediente>> GetAllAsync()
    {
        return await _context.Set<HamburguesaIngrediente>().ToListAsync();

    }

    public virtual async Task<HamburguesaIngrediente> GetByIdAsync(int idHamburguesa, int idIngrediente)
    {
        return await _context.Set<HamburguesaIngrediente>().FirstOrDefaultAsync(x=>x.Hamburguesa_id==idHamburguesa && x.Ingrediente_id==idIngrediente);
    }

    public virtual void Remove(HamburguesaIngrediente entity)
    {
        _context.Set<HamburguesaIngrediente>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<HamburguesaIngrediente> entities)
    {
        _context.Set<HamburguesaIngrediente>().RemoveRange(entities);
    }

    public virtual void Update(HamburguesaIngrediente entity)
    {
        _context.Set<HamburguesaIngrediente>()
            .Update(entity);
    }
    public virtual async Task<(int totalRegistros, IEnumerable<HamburguesaIngrediente> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<HamburguesaIngrediente>().CountAsync();
        var registros = await _context.Set<HamburguesaIngrediente>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
