using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuseoDescalzos.Models;

namespace MUSEO_DE_LOS_DESCALZOS.ViewModel
{
    public abstract class VentaBase 
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? Tipo { get; set; }
        public decimal Monto { get; set; }
    } 
}