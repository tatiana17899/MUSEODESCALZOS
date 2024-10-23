using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUSEODESCALZOS.ViewModel
{
    public class CarritoViewModel
    {
        public List<CarritoEventoViewModel> Eventos { get; set; } 
        public decimal Total { get; set; } 
        public int     vidcliente { get; set; } 
        public long    IDCliente { get; set; } 
        public int     IDEvento { get; set; }
                        
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? NumTarjeta { get; set; }
        public string? Pais { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string? TipoDoc { get; set; }
        public string? NumDoc { get; set; }
        public string? Titular { get; set; }
    }
    public class CarritoEventoViewModel
    {
        public int IDEvento { get; set; }
        public string? NombreEvento { get; set; }
        public string? Descripción { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioUnitario_adultomayor { get; set; }
        public decimal PrecioUnitario_estudiante { get; set; }
        public string?  CorreoElectronico { get; set; }
        public string?  NumTarjeta { get; set; }

        // Mantener la propiedad vidcliente en el evento
        public int vidcliente { get; set; } // ID del cliente para el evento específico

        public int CantidadAdulto { get; set; }
        public int CantidadAdultoMayor { get; set; }
        public int CantidadEscolar { get; set; }
        public int Cantidad { get; set; } // Cantidad de entradas
        public int CantidadNinos { get; set; }

        // Cálculo del precio total
        public decimal PrecioTotal => (CantidadAdulto * PrecioUnitario) +
                                      (CantidadEscolar * PrecioUnitario_estudiante) +
                                      (CantidadAdultoMayor * PrecioUnitario_adultomayor);
    }
}