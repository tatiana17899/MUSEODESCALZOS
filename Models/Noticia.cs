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
    }
}
