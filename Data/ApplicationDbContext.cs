using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MUSEODESCALZOS.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<MUSEODESCALZOS.Actividades> DataActividades { get;set; }
    public DbSet<MUSEODESCALZOS.Alquiler> DataAlquiler { get;set; }
    public DbSet<MUSEODESCALZOS.Calificación_Noticia> DataCalificación_Noticia { get;set; }
    public DbSet<MUSEODESCALZOS.Cliente> DataCliente { get;set; }
    public DbSet<MUSEODESCALZOS.Evento> DataEvento { get;set; }
    public DbSet<MUSEODESCALZOS.Guía> DataGuía { get;set; }
    public DbSet<MUSEODESCALZOS.Imagen_Alquiler> DataImagen_Alquiler { get;set; }
    public DbSet<MUSEODESCALZOS.Noticia> DataNoticia { get;set; }
    public DbSet<MUSEODESCALZOS.PedidoAlquiler> DataPedidoAlquiler { get;set; }
    public DbSet<MUSEODESCALZOS.PedidoEvento> DataPedidoEvento { get;set; }
    public DbSet<MUSEODESCALZOS.PedidoVisita> DataPedidoVisita { get;set; }
    public DbSet<MUSEODESCALZOS.Usuario > DataUsuario { get;set; }
}
