using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuseoDescalzos.Models;

namespace MUSEODESCALZOS.ViewModel
{
    public class ConfirmacionPagoViewModel
    {
        public Cliente? Cliente { get; set; }
        public List<PedidoEvento>? Eventos { get; set; }
    }
}