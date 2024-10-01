using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MUSEODESCALZOS.Models;
using MUSEODESCALZOS.Data;

namespace MUSEODESCALZOS.Controllers
{
    public class EventoController : Controller
    {
        private readonly ILogger<EventoController> _logger;
        private readonly ApplicationDbContext _context;

        public EventoController(ILogger<EventoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Listar()
        {
            var listaEvento = _context.DataEvento.ToList(); // Obtener la lista de alquileres
            return View(listaEvento); // Pasar la lista a la vista  
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}