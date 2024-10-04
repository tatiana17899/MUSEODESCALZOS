using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


namespace MuseoDescalzos.Models
{
    [Table("tb_PedidoVisita")]
    public class PedidoVisita
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDPedidoVisit { get; set; }
        [ForeignKey("Cliente")]
        public long IDCliente { get; set; }
        public Cliente Cliente { get; set; } = default!;
        public string? Detalle { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioTotal { get; set; }
        [ForeignKey("Guía")]
        public long IDGuía { get; set; }
        public Guía? Guía { get; set; } = default!;
        //public bool Estado { get; set; } = false;

    }
}