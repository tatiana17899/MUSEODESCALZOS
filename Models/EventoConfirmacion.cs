using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUSEODESCALZOS.Models
{
    public class EventoConfirmacion
    {
         public string NombreEvento { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}