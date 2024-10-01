using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MUSEODESCALZOS.Models;

namespace MUSEODESCALZOS.Controllers
{
    public class TiendaController : Controller
    {
        private readonly ILogger<TiendaController> _logger;

        public TiendaController(ILogger<TiendaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}