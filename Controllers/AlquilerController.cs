using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEODESCALZOS.Models;
using MUSEODESCALZOS.Data;
using Microsoft.EntityFrameworkCore;


namespace MUSEODESCALZOS.Controllers
{
    public class AlquilerController : Controller
    {
        private readonly ILogger<AlquilerController> _logger;
        private readonly ApplicationDbContext _context;

        public AlquilerController(ILogger<AlquilerController> logger, ApplicationDbContext context)
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
            // Incluir las imágenes relacionadas al obtener los alquileres
            var listaAlquiler = _context.DataAlquiler
                                        .Include(a => a.Imagenes) // Incluir las imágenes asociadas
                                        .ToList(); // Obtener la lista de alquileres con las imágenes

            return View(listaAlquiler); // Pasar la lista a la vista

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}