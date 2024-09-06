using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    public class Cliente
    {
        public int IDCliente { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos{ get; set; }
        public string? TipoDoc { get; set; }
        public string? NumDoc { get; set; }
        public string? Pais { get; set; }
        public string? NumTarjeta { get; set; }
        public string? Titular { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IDUsuario { get; set; }
    }
}