using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MuseoDescalzos.Models;

namespace MUSEODESCALZOS.ViewModel
{
    public class PagoVisitaViewModel
    {
        public long IDCliente { get; set; }
        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Escribe correctamente el nombre")]
        public string? Nombres { get; set; }
        [Required(ErrorMessage = "Los Apellidos son obligatorios.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Escribe correctamente los apellidos")]
        public string? Apellidos { get; set; }
        [Required(ErrorMessage = "Selecciona el tipo de documento.")]
        public string? TipoDoc { get; set; }
        [Required(ErrorMessage = "Escriba el numero de su documento de identificación")]
        public string? NumDoc { get; set; }
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string? Email { get; set; }
        public string? Pais { get; set; }
        public decimal Total { get; set; }
        public string? MetodoPago { get; set; }
        public List<ReservarVisitaViewModel> Visitas { get; set; } = new List<ReservarVisitaViewModel>(); // Mantener como está
        public List<PedidoVisita>? PedidosVisitas { get; set; } // Renombrado aquí
        public string? ImagenUrl { get; set; }
        public string? NumeroTarjeta { get; set; }
        public string? NombreTarjeta { get; set; }
        public string? ExpiracionMM { get; set; }
        public string? ExpiracionYY { get; set; }
        public string? CVV { get; set; }

        public string? Token { get; set; }
        public PedidoVisita? PedidoVisita { get; set; }
    }
}
