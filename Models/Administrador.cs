using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Crypto.Generators;
namespace MuseoDescalzos.Models
{
    [Table("tb_Administrador")]
    public class Administrador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDAdministrador { get; set; }

        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? NumDoc { get; set; }
        public string? Distrito { get; set; }
        public string? Provincia { get; set; }
        public string? Direccion { get; set; }
        public string? Contraseña { get; set; }
        public string? Imagen { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpiration { get; set; }
        public void SetPassword(string password)
        {
            Contraseña = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Contraseña);
        }
    }
}