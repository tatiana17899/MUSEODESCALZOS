using Microsoft.AspNetCore.Mvc;
using MUSEODESCALZOS.Data; // Asegúrate de tener la referencia correcta a tu DbContext
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

        // Acción para listar las noticias
        public IActionResult Index()
        {
            var noticias = _context.DataNoticia.ToList();
            return View("../Admin/ListadoNoticias", noticias); // La vista se encuentra en Views/Admin/ListadoNoticias.cshtml
        }

        // Acción para mostrar la vista de creación de una nueva noticia
        public IActionResult Create()
        {
            return View("../Admin/Create"); // Actualiza la ruta aquí
        }

        // Acción para manejar el envío del formulario de creación de noticia
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                // Convertir la fecha a UTC
                noticia.FechaPublicación = DateTime.UtcNow; // Asignar la fecha actual en UTC

                // Aquí puedes manejar la lógica de subida de archivos si es necesario
                // Por ejemplo, si usas un servicio para guardar la imagen, podrías hacer algo como esto:
                // noticia.Rutalmagen = await SaveImageAndGetPath(noticia.Rutalmagen);

                _context.DataNoticia.Add(noticia); // Agrega la nueva noticia al contexto
                _context.SaveChanges(); // Guarda los cambios en la base de datos
                return RedirectToAction(nameof(Index)); // Redirige al índice después de crear
            }
            return View(noticia); // Vuelve a mostrar el formulario si hay errores
        }

        // Acción para editar una noticia
        public IActionResult Edit(long id)
        {
            var noticia = _context.DataNoticia.Find(id);
            if (noticia == null)
            {
                return NotFound(); // Retorna un 404 si no se encuentra la noticia
            }
            return View("../Admin/Edit", noticia); // Cambia aquí la ruta a la vista
        }

        // Acción para manejar el envío del formulario de edición
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                // Convertir la fecha a UTC si es necesario, por ejemplo, si se actualiza la fecha
                noticia.FechaPublicación = DateTime.UtcNow; // Asignar la fecha actual en UTC

                _context.Update(noticia); // Actualiza la noticia
                _context.SaveChanges(); // Guarda los cambios
                return RedirectToAction(nameof(Index)); // Redirige al índice
            }
            return View("../Admin/Edit", noticia); // Asegúrate de usar la ruta correcta aquí también
        }

        // Acción para eliminar una noticia
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var noticia = await _context.DataNoticia.FindAsync(id);
            if (noticia != null)
            {
                _context.DataNoticia.Remove(noticia);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); // Cambia según la acción que deseas redirigir
        }
    }
}
