
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MUSEODESCALZOS.Models;
using MUSEODESCALZOS.Data;

namespace MUSEODESCALZOS.Controllers
{
    
    public class DetalleAlquilerController : Controller
    {
        private readonly ILogger<DetalleAlquilerController> _logger;
        private readonly ApplicationDbContext _context;
        public DetalleAlquilerController(ILogger<DetalleAlquilerController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detalle(long IDAlquileres)
        {
            var alquiler = _context.DataAlquiler.Find(IDAlquileres);

            if (alquiler == null)
            {
                return NotFound(); // Maneja el caso donde no se encuentra el alquiler
            }

            return View(alquiler);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}