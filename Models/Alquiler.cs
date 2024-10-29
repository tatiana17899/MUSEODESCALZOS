using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    [Table("tb_Alquiler")]
    public class Alquiler
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDAlquileres { get; set; }

        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public int Capacidad { get; set; }
        public string? Caracteristicas { get; set; }
        public DateTime Hora_Disponible { get; set; }
        public bool Disponible { get; set; }
        [NotMapped] // Evita que se mapee a la base de datos
        public List<IFormFile>? ImagenesFiles { get; set; }

        public List<Imagen_Alquiler> Imagenes { get; set; } = new List<Imagen_Alquiler>();
        public PedidoAlquiler PedidoAlquiler { get; set; } = default!;
    }
}