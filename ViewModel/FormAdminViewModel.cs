using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuseoDescalzos.Models;

namespace MUSEO_DE_LOS_DESCALZOS.ViewModel
{
    public class FormAdminViewModel
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? TipVenta { get; set; }
    }
}