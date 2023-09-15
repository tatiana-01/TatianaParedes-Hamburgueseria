using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class IngredienteAllDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal stock { get; set; }   
        public ICollection<HamburguesaIngredienteDTO> HamburguesaIngredientes { get; set; }
    }
}