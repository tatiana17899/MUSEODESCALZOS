using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    [Table("tb_Calificacion")]
    public class Calificación_Noticia
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDCalifNoticia { get; set; }
        [ForeignKey("Califcacion")]
        public long IDCalifcacion { get; set; }
        public Calificación_Noticia? calificación_Noticia { get; set; }= default!;
        [ForeignKey("Noticia")]
        public long IDNoticia { get; set; }
        public Noticia? Noticia { get; set; }= default!;
        [ForeignKey("Usuario")]
        public string?  IDUsuario { get; set; }
        public Usuario? Usuario { get; set; }= default!;
        public string? Tipo { get; set; }

    }
}