using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities;
    public class Hamburguesa:BaseEntity
    {
        public string Nombre { get; set; }
        public Categoria Categoria { get; set; }
        public int Categoria_id { get; set; }   
        public decimal Precio { get; set; }
        public Chef Chef { get; set; }
        public int Chef_id { get; set; }
        public ICollection<HamburguesaIngrediente> HamburguesaIngredientes { get; set; }
    }
