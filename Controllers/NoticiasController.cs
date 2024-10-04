using Microsoft.AspNetCore.Mvc;
using MUSEODESCALZOS.Data; 
using MuseoDescalzos.Models;
using System.Linq;

namespace MuseoDescalzos.Controllers
{
    public class NoticiasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoticiasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ListarNoticias()
        {
            var noticias = _context.DataNoticia.ToList();
            return View(noticias);
        }

        public IActionResult DetalleNoticia(long id)
        {
            var noticia = _context.DataNoticia.Find(id);
            if (noticia == null)
            {
                return NotFound();
            }
            return View(noticia);
        }
    }
}
