using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    public class Alquiler
    {
        public int IDAlquileres { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public int Capacidad { get; set; }
        public string? Caracteristicas { get; set; }
        public DateTime Hora_Disponible { get; set; }
        public bool Disponible { get; set; }

        public List<Imagen_Alquiler> Imagenes { get; set; } = new List<Imagen_Alquiler>();
    }
}