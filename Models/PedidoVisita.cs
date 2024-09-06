using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    public class PedidoVisita
    {
        public int IDPedidoVisit { get; set; }
        public int IDCliente { get; set; }
        public string? Detalle { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioTotal { get; set; }
        public int IDGuia { get; set; }

    }
}