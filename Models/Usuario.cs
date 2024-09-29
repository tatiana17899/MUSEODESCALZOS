using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace MuseoDescalzos.Models
{
    [Table("tb_Usuario")]
    public class Usuario: IdentityUser
    {
        public string? usuario { get; set; }
        public string? Rutalmagen { get; set; }
        public Cliente Cliente { get; set; } = default!;
        public Calificación_Noticia Calificación_Noticia { get; set; } = default!;
    }
}