using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    public class Noticia
    {
        public int IDNoticia { get; set; }
        public string? Titulo { get; set; }
        public string? Descripción { get; set; }
        public string? Nombrelmagen { get; set; }
        public string? Rutalmagen { get; set; }
        public DateTime FechaPublicación { get; set; }
    }
}