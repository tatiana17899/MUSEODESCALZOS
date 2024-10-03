using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
     [Table("tb_Tarea")]
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDTarea { get; set; }
        [ForeignKey("Guía")]
        public long GuíaID { get; set; }
        public Guía Guía { get; set; } = default!;
        public string Descripción { get; set; } = default!;
        public bool Estado { get; set; } = false;
    }
}