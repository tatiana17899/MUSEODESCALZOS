using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
        public Calificación_Noticia Calificación_Noticia { get; set; } = default!;
    }
}