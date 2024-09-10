using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MuseoDescalzos.Models
{
    [Table("tb_PedidoAlquiler")]
    public class PedidoAlquiler
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDPedidoAlq { get; set; }

        public int IDCliente { get; set; }
        public int IDAlquiler { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora_Inicio { get; set; }
        public DateTime Hora_Fin { get; set; }
        public int CantPersona { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}