using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuseoDescalzos.Models
{
    [Table("tb_Calificacion")]
    public class Calificación_Noticia
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IDCalifNoticia { get; set; }
        
        
        public string? Tipo { get; set; }  
        
        // Relación con la entidad Noticia
        [ForeignKey("Noticia")]
        public long IDNoticia { get; set; }
        public Noticia? Noticia { get; set; } = null!; 
        

        [ForeignKey("Usuario")]
        public long IDUsuario { get; set; }
        public Usuario? Usuario { get; set; } = null!; 
    }
}
