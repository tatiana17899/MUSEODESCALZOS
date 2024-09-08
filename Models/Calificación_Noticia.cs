using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    public class Calificación_Noticia
    {
        public int IDCalifNoticia { get; set; }
        public int IDCalifcacion { get; set; }
        public int IDNoticia { get; set; }
        public int IDUsuario { get; set; }
        public string? Tipo { get; set; }

    }
}