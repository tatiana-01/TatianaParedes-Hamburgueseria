using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces;
    public interface IHamburguesa:IGenericRepository<Hamburguesa>
    {
         Task<object> GetVegetariana();
         Task<IEnumerable<Hamburguesa>> GetHamburguesas(string nombreChef);
         Task<IEnumerable<Hamburguesa>> get9();
           Task<IEnumerable<Hamburguesa>> getAscending();
     Task<IEnumerable<HamburguesaIngrediente>> GetHamburguesasconPanIntegral();
      Task<IEnumerable<Hamburguesa>> GetHamburguesasconPanIntegralFinal();
      Task<Hamburguesa> Primero(int id);
    }
