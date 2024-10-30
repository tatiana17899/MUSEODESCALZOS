using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    [Table("tb_Evento")]
    public class Evento
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDEvento { get; set; }

        public string? Nombre { get; set; }
        public string? Descripción { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Precio { get; set; }
        public int Capacidad { get; set; }
        public string? NombreImagen { get; set; }
        public string? RutalImagen { get; set; }
        [NotMapped] // Evita que se mapee a la base de datos
        public IFormFile? ImagenFile { get; set; }
        public ICollection<PedidoEvento>? PedidoEventos { get; set; }

        public ICollection<Actividades>? Actividades { get; set; }
    }
}