using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuseoDescalzos.Models;

namespace MUSEODESCALZOS.ViewModel
{
    public class ReservarVisitaViewModel
    {
        public DateTime Fecha { get; set; }
        public string Hora { get; set; } = string.Empty;
        public long GuiaID { get; set; }
        public List<Guía> GuiaDisponibles { get; set; } = new List<Guía>();
        public int AdultoMayor { get; set; }
        public int Estudiantes { get; set; }
        public int Ninos { get; set; }
        public string Moneda { get; set; } = string.Empty;
        public decimal PrecioTotal { get; set; }

        public decimal CalcularPrecioTotal()
        {
            decimal precioAdultoMayor = 20.00m;
            decimal precioEstudiantes = 10.00m;
            decimal precioNinos = 0.00m;

            return (AdultoMayor * precioAdultoMayor) + (Estudiantes * precioEstudiantes) + (Ninos * precioNinos);
        }
        public List<PedidoVisita> Pedidos { get; set; } = new List<PedidoVisita>();
    }
}