using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEODESCALZOS.Data;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using MuseoDescalzos.Models;
using Microsoft.EntityFrameworkCore;

namespace MUSEODESCALZOS.Controllers
{
    public class ReservarEvento : Controller
    {
        private readonly ILogger<ReservarEvento> _logger;
        private readonly ApplicationDbContext _context;

        public ReservarEvento(ILogger<ReservarEvento> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var evento = _context.DataEvento.FirstOrDefault(e => e.IDEvento == id);
            if (evento == null)
            {
                return NotFound("No se encontró el evento con el ID proporcionado.");
            }

            var viewModel = new EventoViewModel
            {
                Eventoo = evento,
                vidcliente = 5, 
                Cantidad = 1,
                PrecioUnitario = evento.Precio,
                PrecioUnitario_adultomayor = evento.Precio * 0.70m,
                PrecioUnitario_estudiante = evento.Precio * 0.30m
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize] 
        public async Task<IActionResult> Reservar(EventoViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); 
            }

            var usuario = User.Identity.Name;

            var cliente = await _context.DataCliente.FirstOrDefaultAsync(c => c.NumDoc == usuario);
            if (cliente == null)
            {
                ModelState.AddModelError("", "Cliente no encontrado.");
                return View("Index", model);
            }

            var evento = await _context.DataEvento.FirstOrDefaultAsync(e => e.IDEvento == model.IDEvento);
            if (evento == null)
            {
                ModelState.AddModelError("", "Evento no encontrado.");
                return View("Index", model);
            }

            var pedidoEvento = new PedidoEvento
            {
                IDCliente = cliente.IDCliente,
                IDEvento = evento.IDEvento,  
                Detalle = model.Detalle,
                Cantidad = model.CantidadTotal,
                PrecioUnitario = model.PrecioTotal,
                PrecioTotal = model.PrecioTotal,
                Fecha = DateTime.Now
            };

            try
            {
                _context.DataPedidoEvento.Add(pedidoEvento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error al guardar el pedido: {0}", ex.Message);
                ModelState.AddModelError("", "No se pudo realizar la reserva. Intente nuevamente.");
                return View("Index", model);
            }

            return RedirectToAction("Index"); 
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
