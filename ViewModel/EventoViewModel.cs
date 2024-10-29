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
        public Evento Eventoo { get; set; }
        public string? Evento { get; set; }
        public int IDEvento { get; set; }
        public long IDPedidoEvento { get; set; }
        public Evento? FormEvento { get; set; }
        public List<Evento>? ListEvento { get; set; }
        public List<Guía>? ListGuia { get; set; }
        public List<Actividades>? ListActividades { get; set; } // Nueva propiedad para actividades
 
        public string? Detalle { get; set; }
        public int Cantidad { get; set; }
        public int vidcliente  { get; set; }
        public string? NombreEvento { get; set; }
        public decimal PrecioUnitario { get; set; } = 0;
        public decimal PrecioUnitario_adultomayor { get; set; } = 0;
        public decimal PrecioUnitario_estudiante { get; set; } = 0;
        public decimal PrecioTotal => Cantidad * PrecioUnitario;
        public int CantidadAdulto { get; set; }  
        public int CantidadAdultoMayor { get; set; }
        public int CantidadEscolar { get; set; }
        public int CantidadNinos { get; set; }

        public int CantidadTotal => CantidadAdulto + CantidadAdultoMayor + CantidadEscolar;

      

    }
}