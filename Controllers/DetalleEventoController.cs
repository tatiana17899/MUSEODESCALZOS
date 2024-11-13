using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MUSEODESCALZOS.Data;

namespace MUSEODESCALZOS.Controllers
{
    public class DetalleEventoController : Controller
    {
        private readonly ILogger<EventoController> _logger;
        private readonly ApplicationDbContext _context;

        public DetalleEventoController(ILogger<EventoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detalle(long IDEvento)
        {
            var evento = _context.DataEvento
                        .Include(e => e.Actividades) 
                        .ThenInclude(a => a.Guía) 
                        .FirstOrDefault(e => e.IDEvento == IDEvento);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}