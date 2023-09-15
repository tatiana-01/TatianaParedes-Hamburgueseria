using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.Seeding;
    public class SeedingInicial
    {
          public static void Seed (ModelBuilder modelBuilder){
            var Pan= new Ingrediente(){
                Id=1,
                Nombre="Pan",
                Descripcion="Pan",
                Precio=456,
                stock=1
            };
            var clasica= new Hamburguesa(){
                Id=1,
                Nombre="Clásica",
                Chef_id=1,
                Categoria_id=1
            };
            var chef= new Chef(){
                Id=1,
                Nombre="Tomas",
                Especialidad="Carnes"
            };
            var categoria= new Categoria(){
                Id=1,
                Nombre="Gourmet",
                Descripcion="Gourmet"
            };
            modelBuilder.Entity<Ingrediente>().HasData(Pan);
            modelBuilder.Entity<Hamburguesa>().HasData(clasica);
            modelBuilder.Entity<Chef>().HasData(chef);
            modelBuilder.Entity<Categoria>().HasData(categoria);
        }
    }
