using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEODESCALZOS.Data;
using MuseoDescalzos.Models;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using System.Linq;

namespace MUSEODESCALZOS.Controllers
{
    public class GuiaDashboard : Controller
    {
        private readonly ILogger<GuiaDashboard> _logger;
        private readonly ApplicationDbContext _context;

        public GuiaDashboard(ILogger<GuiaDashboard> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Vista principal
        public IActionResult Index()
        {
            var usuario = User.Identity.Name; 
            var tareas = _context.DataTareas.Where(t => t.Guía.Email == usuario).ToList();
            var tareasCompletadas = tareas.Count(t => t.Estado);
            var tareasNoCompletadas = tareas.Count(t => !t.Estado);
            
            var viewModel = new GuiaDashboardViewModel
            {
                Tareas = tareas,
                TareasCompletadas = tareasCompletadas,
                TareasNoCompletadas = tareasNoCompletadas
            };
            return View(viewModel);
        }

        public IActionResult Visitas()
        {
            var usuario = User.Identity.Name; 
            var visitas = _context.DataPedidoVisita.Where(v => v.Guía.Email == usuario).ToList();
            var viewModel = new GuiaDashboardViewModel
            {
                Visitas = visitas
            };

            return View(viewModel);
        }

        // Método para actualizar el estado de la tarea
        [HttpPost]
        public IActionResult ActualizarTarea(long id, bool estado)
        {
            var tarea = _context.DataTareas.Find(id);
            if (tarea != null)
            {
                tarea.Estado = estado;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Método para actualizar el estado de la visita
        [HttpPost]
        public IActionResult ActualizarVisita(long id, bool estado)
        {
            var visita = _context.DataPedidoVisita.Find(id);
            if (visita != null)
            {
                //visita.Estado = estado;
                _context.SaveChanges();
            }
            return RedirectToAction("Visitas");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
