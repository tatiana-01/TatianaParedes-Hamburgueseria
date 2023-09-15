using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities;
    public class Ingrediente:BaseEntity     
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal stock { get; set; }   
        public ICollection<HamburguesaIngrediente> HamburguesaIngredientes { get; set; }
    }
