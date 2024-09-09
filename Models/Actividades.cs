using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    [Table("tb_Actividades")]
    public class Actividades
    {
        public int IDActividades { get; set; }
        public DateTime Hora_Inicial { get; set; }
        public DateTime Hora_Final { get; set; }
        public string? Actividad { get; set; }
        public int IDGuía { get; set; }
        public int IDEvento { get; set; }
    }
}