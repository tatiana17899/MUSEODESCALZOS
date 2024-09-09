using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    [Table("tb_Guía")]
    public class Guía
    {
        public int IDGuía { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Email { get; set; }
        public string? TipPago { get; set; }
        public decimal Sueldo { get; set; }
    }
}