
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MUSEODESCALZOS.Models;
using MUSEODESCALZOS.Data;
using Microsoft.EntityFrameworkCore;

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
            var alquiler = _context.DataAlquiler.Include(a => a.Imagenes)
                            .FirstOrDefault(a => a.IDAlquileres == IDAlquileres);

            if (alquiler?.Imagenes == null || !alquiler.Imagenes.Any())
            {
                return NotFound();// Manejar caso donde no hay imágenes
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