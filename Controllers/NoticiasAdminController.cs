using Microsoft.AspNetCore.Mvc;
using MUSEODESCALZOS.Data; 
using MuseoDescalzos.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MuseoDescalzos.Controllers
{
    public class NoticiasAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoticiasAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var noticias = _context.DataNoticia.ToList();
            return View("../Admin/ListadoNoticias", noticias);
        }

        public IActionResult Create()
        {
            return View("../Admin/Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                noticia.FechaPublicación = DateTime.UtcNow;
                _context.DataNoticia.Add(noticia);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(noticia);
        }

        public IActionResult Edit(long id)
        {
            var noticia = _context.DataNoticia.Find(id);
            if (noticia == null)
            {
                return NotFound();
            }
            return View("../Admin/Edit", noticia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                noticia.FechaPublicación = DateTime.UtcNow;
                _context.Update(noticia);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("../Admin/Edit", noticia);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var noticia = await _context.DataNoticia.FindAsync(id);
            if (noticia != null)
            {
                _context.DataNoticia.Remove(noticia);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
