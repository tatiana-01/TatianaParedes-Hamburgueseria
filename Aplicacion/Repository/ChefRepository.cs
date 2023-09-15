using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;
public class ChefRepository : GenericRepository<Chef>, IChef
{
    private readonly DbAppContext _context;
    public ChefRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
