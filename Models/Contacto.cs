using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MuseoDescalzos.Models
{
    [Table("t_contacto")]
    public class Contacto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {   get; set;}
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        [NotMapped]
        public string? Contrasena { get; set; }
    }
}