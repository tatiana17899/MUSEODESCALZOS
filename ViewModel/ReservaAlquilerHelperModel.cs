using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MuseoDescalzos.Models;

namespace MUSEO_DE_LOS_DESCALZOS.ViewModel
{
    public class ReservaAlquilerHelperModel
{
    public long IDReserva { get; set; }
    public long IDAlquileres { get; set; }
    public string NumeroDocumento { get; set; }
    public int Cantidad { get; set; } // Supongamos que la cantidad es un entero
    public string Fecha { get; set; } // Usar string si solo lo necesitas para mostrar
    public string HoraInicio { get; set; }
    public string HoraFin { get; set; }
    public decimal Precio { get; set; } // Suponiendo que el precio es decimal
}
}