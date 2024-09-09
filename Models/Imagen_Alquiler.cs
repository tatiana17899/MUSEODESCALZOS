using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    [Table("tb_ImagenAlquiler")]
    public class Imagen_Alquiler
    {
        public int IDimgAlquiler { get; set; }
        public int IDAlquiler { get; set; }
        public string? Rutalmagen { get; set; }
        public string? Nombrelmagen { get; set; }
    }
}