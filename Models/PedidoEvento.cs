using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    [Table("tb_PedidoEvento")]
    public class PedidoEvento
    {
        public int IDPedidoEvento { get; set; }
        public int IDCliente { get; set; }
        public int IDEvento { get; set; }
        public string? Detalle { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}