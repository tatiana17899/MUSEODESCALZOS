using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEODESCALZOS.Data;
using MuseoDescalzos.Models;
using Microsoft.EntityFrameworkCore;

namespace MuseoDescalzos.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ApplicationDbContext context, ILogger<AdminController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Profile(long id) 
        {
            var administrador = _context.DataAdministrador.Find(id); 
            if (administrador == null)
            {
                return NotFound(); 
            }
            return View(administrador); 
        }

        [HttpPost]
        public IActionResult UpdateProfile(Administrador administrador) 
        {
            if (ModelState.IsValid) 
            {
                _context.DataAdministrador.Update(administrador); 
                _context.SaveChanges(); 
                return RedirectToAction("Profile", new { id = administrador.IDAdministrador }); 
            }
            return View(administrador); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
