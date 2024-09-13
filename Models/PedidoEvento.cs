using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    [Table("tb_PedidoEvento")]
    public class PedidoEvento
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDPedidoEvento { get; set; }
        [ForeignKey("Cliente")]
        public long IDCliente { get; set; }
        public Cliente Cliente { get; set; } = default!;
        [ForeignKey("Evento")]
        public long IDEvento { get; set; }
        public Evento Evento { get; set; } = default!;
        public string? Detalle { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime Fecha { get; set; } 
    }
}