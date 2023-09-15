using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;
public class HamburguesaRepository : GenericRepository<Hamburguesa>, IHamburguesa
{
    private readonly DbAppContext _context;
    public HamburguesaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
