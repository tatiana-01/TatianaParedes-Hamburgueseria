using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class HamburguesaRepository : GenericRepository<Hamburguesa>, IHamburguesa
{
    private readonly DbAppContext _context;
    public HamburguesaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
     public override async Task<(int totalRegistros, IEnumerable<Hamburguesa> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<Hamburguesa>().CountAsync();
        var registros = await _context.Set<Hamburguesa>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Include(x=>x.HamburguesaIngredientes)
            .ToListAsync();
        return (totalRegistros, registros);
    }

    public override async Task<Hamburguesa> GetByIdAsync(int id)
    {
        return await _context.Set<Hamburguesa>().Include(x=>x.HamburguesaIngredientes).FirstOrDefaultAsync(x=>x.Id==id);
    }

    public async Task<object> GetVegetariana()
    {

         var hamburguesas= (from hamburguesa in _context.Hamburguesas join categoria in _context.Categorias on hamburguesa.Categoria_id equals categoria.Id select new {
            Nombre =hamburguesa.Nombre,
            categoria= categoria.Nombre,
            Precio =hamburguesa.Precio,
            Chef_id= hamburguesa.Chef_id,
            HamburguesaIngredientes=hamburguesa.HamburguesaIngredientes,
         });//.Include(p=>p.HamburguesaIngredientes);
        var veggies= hamburguesas.Where(p=>p.categoria.ToLower()=="vegetariana");
        return (veggies);
    }

   public async Task<IEnumerable<Hamburguesa>> GetHamburguesas(string nombreChef)
    {
        var chefID= _context.Chefs.First(p=>p.Nombre.ToLower().Contains(nombreChef.ToLower()));
        
            var hamburguesa= _context.Hamburguesas.Where(x=>x.Chef_id == chefID.Id);
            
   
        return hamburguesa;
    }

     public async Task<IEnumerable<Hamburguesa>> get9(){
        return _context.Hamburguesas.Where(p=>p.Precio<=9).Include(p=>p.HamburguesaIngredientes);
     }

      public async Task<IEnumerable<Hamburguesa>> getAscending(){
        return _context.Hamburguesas.OrderBy(p=>p.Precio).Include(p=>p.HamburguesaIngredientes);
     }
public async Task<IEnumerable<HamburguesaIngrediente>> GetHamburguesasconPanIntegral()
    {
        var IngredienteID= await _context.Ingredientes.FirstOrDefaultAsync(p=>p.Nombre.ToLower().Contains("pan integral"));
        List<Hamburguesa> hamburguesas= new List<Hamburguesa>();
            var hamburguesa= _context.HamburguesaIngredientes.Where(p=>p.Ingrediente_id==IngredienteID.Id);
        return hamburguesa.AsEnumerable();
    }
public async Task<IEnumerable<Hamburguesa>> GetHamburguesasconPanIntegralFinal()
    {
        List<Hamburguesa>hamburguesas= new List<Hamburguesa>();
        var HamburguesaIngrediente= await GetHamburguesasconPanIntegral();
        foreach (var item in HamburguesaIngrediente)
        {
            var hamburguesa= await _context.Hamburguesas.FirstOrDefaultAsync(x=>x.Id==item.Hamburguesa_id);
            hamburguesas.Add(hamburguesa);
        }
        return hamburguesas.AsEnumerable();
    }

    public async Task<Hamburguesa> Primero(int id)
    {
        return _context.Hamburguesas.FirstOrDefault(x=>x.Id==id);
    }
    



}
