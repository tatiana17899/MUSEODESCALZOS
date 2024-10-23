using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MUSEODESCALZOS.Data;
using MUSEODESCALZOS.ViewModel;
using MuseoDescalzos.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MUSEODESCALZOS.Controllers
{
    public class ReservarVisitaController : Controller
    {
        private readonly ILogger<ReservarVisitaController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser > _userManager;

        public ReservarVisitaController(ILogger<ReservarVisitaController> logger, ApplicationDbContext context, UserManager<IdentityUser > userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var guiasDisponibles = _context.DataGuía.Where(g => g.Disponible).ToList();
            var viewModel = new ReservarVisitaViewModel
            {
                GuiaDisponibles = guiasDisponibles
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReservation(ReservarVisitaViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cliente = _context.DataCliente.FirstOrDefault(c => c.UserId == userId);
            if (cliente == null)
            {
                ModelState.AddModelError("", "Cliente no encontrado.");
                return View("Index", model);
            }

            if (ModelState.IsValid)
            {
                var precioTotal = model.CalcularPrecioTotal();
                
                var pedidoVisita = new PedidoVisita
                {
                    Fecha = model.Fecha.ToUniversalTime(),
                    Cantidad = model.AdultoMayor + model.Estudiantes + model.Ninos,
                    PrecioUnitario = precioTotal / (model.AdultoMayor + model.Estudiantes + model.Ninos),
                    PrecioTotal = precioTotal,
                    IDGuía = model.GuiaID != 0 ? model.GuiaID : -1,
                    IDCliente = cliente.IDCliente
                };

                _context.DataPedidoVisita.Add(pedidoVisita);
                await _context.SaveChangesAsync();

                return RedirectToAction("Summary", new { id = pedidoVisita.IDPedidoVisit });
            }

            model.GuiaDisponibles = _context.DataGuía.Where(g => g.Disponible).ToList();
            return View("Index", model);
        }

        public IActionResult Summary(long id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cliente = _context.DataCliente.FirstOrDefault(c => c.UserId == userId);
            
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            var pedidos = _context.DataPedidoVisita
                .Include(p => p.Guía)
                .Where(p => p.Cliente.IDCliente == cliente.IDCliente) 
                .ToList();

            var viewModel = new ReservarVisitaViewModel
            {
                GuiaDisponibles = _context.DataGuía.Where(g => g.Disponible).ToList(),
                Pedidos = pedidos 
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteReservation(long id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cliente = _context.DataCliente.FirstOrDefault(c => c.UserId == userId);

            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            var pedidoVisita = _context.DataPedidoVisita.FirstOrDefault(p => p.IDPedidoVisit == id);
            if (pedidoVisita == null)
            {
                return NotFound();
            }

            _context.DataPedidoVisita.Remove(pedidoVisita);
            _context.SaveChanges();

            return RedirectToAction("Summary");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}