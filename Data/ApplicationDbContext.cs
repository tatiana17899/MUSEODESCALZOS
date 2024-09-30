using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MUSEODESCALZOS.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<MuseoDescalzos.Models.Actividades> DataActividades { get;set; }
    public DbSet<MuseoDescalzos.Models.Alquiler> DataAlquiler { get;set; }
    public DbSet<MuseoDescalzos.Models.Cliente> DataCliente { get;set; }
    public DbSet<MuseoDescalzos.Models.Evento> DataEvento { get;set; }
    public DbSet<MuseoDescalzos.Models.Guía> DataGuía { get;set; }
    public DbSet<MuseoDescalzos.Models.Imagen_Alquiler> DataImagen_Alquiler { get;set; }
    public DbSet<MuseoDescalzos.Models.Noticia> DataNoticia { get;set; }
    public DbSet<MuseoDescalzos.Models.PedidoAlquiler> DataPedidoAlquiler { get;set; }
    public DbSet<MuseoDescalzos.Models.PedidoEvento> DataPedidoEvento { get;set; }
    public DbSet<MuseoDescalzos.Models.PedidoVisita> DataPedidoVisita { get;set; }
    public DbSet<MuseoDescalzos.Models.Tarea > DataTareas { get;set; }
    public DbSet<MuseoDescalzos.Models.Contacto> DataContacto { get; set; }
}