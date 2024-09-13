using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    [Table("tb_Actividades")]
    public class Actividades
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDActividades { get; set; }
        public DateTime Hora_Inicial { get; set; }
        public DateTime Hora_Final { get; set; }
        public string? Actividad { get; set; }
        public Guía? Guía { get; set; } = default!;

        [ForeignKey("Evento")]
        public long IDEvento { get; set; }
        public Evento? Evento { get; set; } = default!;

    }
}