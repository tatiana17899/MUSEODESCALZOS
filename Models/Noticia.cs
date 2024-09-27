using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuseoDescalzos.Models
{
    [Table("tb_Noticia")]
    public class Noticia
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDNoticia { get; set; }

        public string? Titulo { get; set; }
        public string? Descripción { get; set; }
        public string? Rutalmagen { get; set; }
        public DateTime FechaPublicación { get; set; }

        // Relación con Calificación_Noticia
        public ICollection<Calificación_Noticia>? Calificaciones { get; set; } = new List<Calificación_Noticia>();
    }
}
