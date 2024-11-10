using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using MuseoDescalzos.Models;
using MUSEODESCALZOS.Data;
using ClasificacionModelo;

namespace MUSEO_DE_LOS_DESCALZOS.Controllers
{

    public class ContactoAdminController : Controller
    {
        private readonly ILogger<ContactoAdminController> _logger;
        private readonly ApplicationDbContext _context;

        public ContactoAdminController(ILogger<ContactoAdminController> logger,ApplicationDbContext context)
        {
            _logger = logger;
             _context = context;
        }

        public IActionResult Index()
        {
            var contactos = _context.DataContacto.ToList();
            return View(contactos);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}