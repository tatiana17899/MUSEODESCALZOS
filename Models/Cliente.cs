using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MuseoDescalzos.Models
{
    [Table("tb_Cliente")]
    public class Cliente
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDCliente { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos{ get; set; }
        public string? TipoDoc { get; set; }
        public string? NumDoc { get; set; }
        public string? Pais { get; set; }
        public string? NumTarjeta { get; set; }
        public string? Titular { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string StripeCustomerId { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser? User { get; set; }
        public ICollection<PedidoEvento>? PedidoEvento { get; set; }
        public ICollection<PedidoAlquiler>? PedidoAlquiler { get; set; }
        public ICollection<PedidoVisita>? PedidoVisita { get; set; }
    }
}