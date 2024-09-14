using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuseoDescalzos.Models;

namespace MUSEO_DE_LOS_DESCALZOS.ViewModel
{
    public class EventoViewModel: VentaBase
    {
        public Cliente? Cliente { get; set; }
        public string? Evento { get; set; }
        public string? Detalle { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}