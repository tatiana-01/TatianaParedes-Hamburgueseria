

using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    HamburguesaRepository _hamburguesa;
    IngredienteRepository _ingrediente;
    CategoriaRepository _categoria;
    ChefRepository _chef;
    HamburguesaIngredienteRepository _hamburguesaIngrediente;
    private readonly DbAppContext _context;
    public UnitOfWork(DbAppContext context)
    {
        _context = context;
    }
    public IHamburguesa Hamburguesas
    {
        get
        {
            if (_hamburguesa is not null)
            {
                return _hamburguesa;
            }
            return _hamburguesa = new HamburguesaRepository(_context);
        }
    }
    public IIngrediente Ingredientes
    {
        get
        {
            if (_ingrediente is not null)
            {
                return _ingrediente;
            }
            return _ingrediente = new IngredienteRepository(_context);
        }
    }
      public ICategoria Categorias
    {
        get
        {
            if (_categoria is not null)
            {
                return _categoria;
            }
            return _categoria = new CategoriaRepository(_context);
        }
    }
      public IChef Chefs
    {
        get
        {
            if (_chef is not null)
            {
                return _chef;
            }
            return _chef = new ChefRepository(_context);
        }
    }
      public IHamburguesaIngrediente HamburguesaIngredientes
    {
        get
        {
            if (_hamburguesaIngrediente is not null)
            {
                return _hamburguesaIngrediente;
            }
            return _hamburguesaIngrediente = new HamburguesaIngredienteRepository(_context);
        }
    }
    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

}