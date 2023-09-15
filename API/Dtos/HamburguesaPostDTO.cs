using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class HamburguesaPostDTO
    {
         public string Nombre { get; set; }
        public int Categoria_id { get; set; }
        public decimal Precio { get; set; }
        public int Chef_id { get; set; }
    }
}