using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    public class Usuario
    {
        public int IDUsuario { get; set; }
        public string? usuario { get; set; }
        public string? Email { get; set; }
        public string? Contraseña { get; set; }
        public string? Reestablecer { get; set; }
        public string? Nombrelmagen { get; set; }
        public string? Rutalmagen { get; set; }
    }
}