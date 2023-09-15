using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IHamburguesaIngrediente
{
    Task<HamburguesaIngrediente> GetByIdAsync(int idHamburguesa, int idIngrediente);
    Task<IEnumerable<HamburguesaIngrediente>> GetAllAsync();
    IEnumerable<HamburguesaIngrediente> Find(Expression<Func<HamburguesaIngrediente, bool>> expression);
    Task<(int totalRegistros, IEnumerable<HamburguesaIngrediente> registros)> GetAllAsync(int pageIndex, int pageSize, string search);
    void Add(HamburguesaIngrediente entity);
    void AddRange(IEnumerable<HamburguesaIngrediente> entities);
    void Remove(HamburguesaIngrediente entity);
    void RemoveRange(IEnumerable<HamburguesaIngrediente> entities);
    void Update(HamburguesaIngrediente entity);
    HamburguesaIngrediente AddToClassic(int idIngrediente);

}
