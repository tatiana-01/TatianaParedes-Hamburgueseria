using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces;
    public interface IIngrediente:IGenericRepository<Ingrediente>
    {
        Task<(int totalRegistros, IEnumerable<Ingrediente> registros)> GetStock400(int pageIndex, int pageSize, string search);
        Task<(int totalRegistros, IEnumerable<Ingrediente> registros)> GetPrecio2a5(int pageIndex, int pageSize, string search);
        Ingrediente cambioPanPorFresco();
        Ingrediente GetMostExpensive();
        
    }
