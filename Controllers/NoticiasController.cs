using Microsoft.AspNetCore.Mvc;
using MuseoDescalzos.Models;
using System.Collections.Generic;

namespace MuseoDescalzos.Controllers
{
    public class NoticiasController : Controller
    {
     
        public IActionResult ListarNoticias()
        {
            
            var noticias = new List<Noticia>
            {
                new Noticia { IDNoticia = 1, Titulo = "EVENTO MUSEO 'Memory'", Descripción = "Descripción del evento", Rutalmagen = "imagen1.jpg", FechaPublicación = DateTime.Now },
                new Noticia { IDNoticia = 2, Titulo = "Conferencia Histórica", Descripción = "Descripción de la conferencia", Rutalmagen = "imagen2.jpg", FechaPublicación = DateTime.Now }
            };

            return View(noticias);
        }

        
        public IActionResult DetalleNoticia(long id)
        {
            
            var noticia = new Noticia 
            { 
                IDNoticia = 1, 
                Titulo = "EVENTO MUSEO 'Memory'", 
                Descripción = "Descripción detallada del evento", 
                Rutalmagen = "imagen_detalle.jpg", 
                FechaPublicación = DateTime.Now,
                Calificaciones = new List<Calificación_Noticia>
                {
                    new Calificación_Noticia { IDCalifNoticia = 1, Tipo = "Positiva", IDNoticia = 1, IDUsuario = 1 },
                    
                }
            };

            return View(noticia);
        }
    }
}
