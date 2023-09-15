using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;
public class IngredienteRepository : GenericRepository<Ingrediente>, IIngrediente
{
    private readonly DbAppContext _context;
    public IngredienteRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
