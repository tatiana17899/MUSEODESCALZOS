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

        public int IDCalifcacion { get; set; }
        public int IDNoticia { get; set; }
        public int IDUsuario { get; set; }
        public string? Tipo { get; set; }

    }
}