using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    public class PedidoAlquiler
    {
        public int IDPedidoAlq { get; set; }
        public int IDCliente { get; set; }
        public int IDAlquiler { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora_Inicio { get; set; }
        public DateTime Hora_Fin { get; set; }
        public int CantPersona { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}