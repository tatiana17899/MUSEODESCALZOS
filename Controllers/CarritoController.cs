using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using MUSEODESCALZOS.Extensions;
using MuseoDescalzos.Models; 
using System.Threading.Tasks;
using MUSEODESCALZOS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.ComponentModel;

namespace MUSEODESCALZOS.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly ILogger<CarritoController> _logger;
        private readonly ApplicationDbContext _context; 
        private readonly UserManager<IdentityUser> _userManager;
        private const string CarritoSessionKey = "Carrito";

        public CarritoController(ILogger<CarritoController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context; 
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cliente = _context.DataCliente.FirstOrDefault(c => c.UserId == userId); 

            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }
            var carrito = _context.DataPedidoEvento
                .Where(p => p.IDCliente == cliente.IDCliente)
                .ToList();

            var eventoViewModels = carrito.Select(p => new EventoViewModel
            {
                IDEvento = (int)p.IDEvento,
                Cantidad = p.Cantidad,
                PrecioUnitario = p.PrecioUnitario,
                Eventoo = _context.DataEvento.FirstOrDefault(e => e.IDEvento == p.IDEvento)
                
            }).ToList();

            return View(eventoViewModels);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito(EventoViewModel model)
        {
            // Obtener el ID del cliente actual
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cliente = _context.DataCliente.FirstOrDefault(c => c.UserId == userId);

            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            // Verificar si el evento ya existe en el carrito del cliente
            var pedidoExistente = _context.DataPedidoEvento
                .FirstOrDefault(p => p.IDEvento == model.IDEvento && p.IDCliente == cliente.IDCliente);

            if (pedidoExistente != null)
            {
                // Si ya existe, solo actualizamos la cantidad
                pedidoExistente.Cantidad += model.CantidadTotal; // CantidadTotal de adultos, adultos mayores y escolares
                pedidoExistente.PrecioUnitario = model.PrecioUnitario; // Puedes ajustar los precios si es necesario
                _context.Update(pedidoExistente);
            }
            else
            {
                // Si no existe, creamos un nuevo pedido
                var nuevoPedido = new PedidoEvento
                {
                    IDCliente = cliente.IDCliente,
                    IDEvento = model.IDEvento,
                    Cantidad = model.CantidadTotal,
                    PrecioUnitario = model.PrecioUnitario,
                };

                _context.DataPedidoEvento.Add(nuevoPedido);
            }

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Redirigir al carrito
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> EliminarDelCarrito(long idEvento)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cliente = _context.DataCliente.FirstOrDefault(c => c.UserId == userId);

            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            var pedidoAEliminar = await _context.DataPedidoEvento
                .FirstOrDefaultAsync(p => p.IDEvento == idEvento && p.IDCliente == cliente.IDCliente);

            if (pedidoAEliminar != null)
            {
                _context.DataPedidoEvento.Remove(pedidoAEliminar);
                await _context.SaveChangesAsync();
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
