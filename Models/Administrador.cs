using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MuseoDescalzos.Models
{
    [Table("tb_Admin")]
    public class Administrador: IdentityUser
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? NumDoc { get; set; }
        public string? Distrito { get; set; }
        public string? Provincia { get; set; }
        public string? Direccion { get; set; }
        public string? Imagen { get; set; }
        
    }
}