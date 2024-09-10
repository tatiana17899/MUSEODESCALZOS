using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEODESCALZOS.Data;
namespace MuseoDescalzos.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ApplicationDbContext context, ILogger<AdminController> logger){
            _context = context;
            _logger = logger;
        }


        public IActionResult Index()
        {
            var alquileresCount = _context.DataPedidoAlquiler.Count();
            var visitasCount = _context.DataPedidoVisita.Count();
            var eventosCount = _context.DataPedidoEvento.Count(); 

            var model = new DashboardViewModel
            {
                CantidadAlquileres = alquileresCount,
                CantidadVisitas = visitasCount,
                CantidadEventos = eventosCount
            };

            return View(model);

        }
        public IActionResult Profile()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
    public class DashboardViewModel
    {
        public int CantidadAlquileres { get; set; }
        public int CantidadVisitas { get; set; }
        public int CantidadEventos { get; set; }
    }
}