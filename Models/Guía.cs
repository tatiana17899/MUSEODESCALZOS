using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    [Table("tb_Guía")]
    public class Guía
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDGuía { get; set; }

        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Email { get; set; }
        public string? TipPago { get; set; }
        public decimal Sueldo { get; set; }
         public string? ContraseñaGenerada { get; set; } 
        public PedidoVisita PedidoVisita { get; set; } = default!;
        public ICollection<Actividades>? Actividades { get; set; }
        public bool Disponible { get; set; } 
        public ICollection<Tarea>? Tareas { get; set; }

        public static string GenerarContraseña(int longitud = 8)
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(caracteres, longitud).Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
    
}