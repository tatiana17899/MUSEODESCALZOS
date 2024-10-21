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

            // Prepara el modelo para la vista Reservar
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

            var pedidoEvento = new PedidoEvento
            {
                IDCliente = cliente.IDCliente, 
                IDEvento = model.IDEvento,
                Detalle = model.Detalle,
                Cantidad = model.CantidadTotal,
                PrecioUnitario = model.PrecioTotal,
                PrecioTotal = model.PrecioTotal, 
                Fecha = DateTime.Now 
            };


            _context.DataPedidoEvento.Add(pedidoEvento);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
