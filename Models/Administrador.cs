using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MuseoDescalzos.Models
{
    [Table("tb_admin")]
    public class Administrador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idadministrador { get; set; }

        public string? nombres { get; set; }
        public string? apellidos { get; set; }
        public DateTime fechanacimiento { get; set; }
        public string? numdoc { get; set; }
        public string? distrito { get; set; }
        public string? provincia { get; set; }
        public string? direccion { get; set; }
        public string? imagen { get; set; }
    }
}
