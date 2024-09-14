using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    [Table("tb_ImagenAlquiler")]
    public class Imagen_Alquiler
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDimgAlquiler { get; set; }
        [ForeignKey("Alquiler")]
        public long IDAlquiler { get; set; }
        public Alquiler? Alquiler { get; set; }= default!;
        public string? Rutalmagen { get; set; }
    }
}