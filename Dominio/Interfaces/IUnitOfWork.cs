

namespace Dominio.Interfaces;

    public interface IUnitOfWork
    {
        IHamburguesa Hamburguesas {get;}
        IIngrediente Ingredientes {get;}
        IChef Chefs {get;}
        ICategoria Categorias {get;}
        IHamburguesaIngrediente HamburguesaIngredientes {get;}
        Task<int> SaveAsync();
    }

