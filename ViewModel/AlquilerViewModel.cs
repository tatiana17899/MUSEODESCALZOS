using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuseoDescalzos.Models;

namespace MUSEO_DE_LOS_DESCALZOS.ViewModel
{
    public class AlquilerViewModel: VentaBase
    {
        public Cliente? Cliente { get; set; }
        public string? Alquiler { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public int CantPersona { get; set; }
    }
}