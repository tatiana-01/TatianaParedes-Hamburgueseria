using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities;
    public class Chef:BaseEntity
    {
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public ICollection<Hamburguesa> Hamburguesas { get; set; }
    }
