using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia
{
    public class DbAppContext : DbContext
    {
        public DbAppContext(DbContextOptions<DbAppContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           // SeedingInicial.Seed(odelBuilder);
        }
         public DbSet<Hamburguesa> Hamburguesas { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<HamburguesaIngrediente> HamburguesaIngredientes {get; set;}
    }
}