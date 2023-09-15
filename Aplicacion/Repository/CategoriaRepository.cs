using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;
public class CategoriaRepository : GenericRepository<Categoria>, ICategoria
{
    private readonly DbAppContext _context;
    public CategoriaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
}
