using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEODESCALZOS.Data;

namespace MUSEODESCALZOS.Controllers
{
    public class ReservarVisitaController : Controller
    {
        private readonly ILogger<ReservarVisitaController> _logger;
        private readonly ApplicationDbContext _context;

        public ReservarVisitaController(ILogger<ReservarVisitaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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