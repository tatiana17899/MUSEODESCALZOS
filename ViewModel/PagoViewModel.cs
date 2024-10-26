using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using MuseoDescalzos.Models;

namespace MUSEODESCALZOS.ViewModel
{
    public class PagoViewModel
    {
        public long IDCliente { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? TipoDoc { get; set; }
        public string? NumDoc { get; set; }
        public string? Email { get; set; }
        public string? Pais { get; set; }
        public decimal Total { get; set; }
        public string? MetodoPago { get; set; }
        public List<EventoViewModel> Eventos { get; set; } = new List<EventoViewModel>();
        public List<PedidoVisita>? Visitas { get; set; }
        public string? ImagenUrl { get; set; }
        public string? NumeroTarjeta { get; set; }
        public string? NombreTarjeta { get; set; }
        public string? ExpiracionMM { get; set; }
        public string? ExpiracionYY { get; set; }
        public string? CVV { get; set; }

        public string? Token { get; set; }

    }
}