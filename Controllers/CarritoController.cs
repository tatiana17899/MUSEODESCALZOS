using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using MUSEODESCALZOS.Extensions;
using MuseoDescalzos.Models; 
using System.Threading.Tasks;
using MUSEODESCALZOS.Data;

namespace MUSEODESCALZOS.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ILogger<CarritoController> _logger;
        private readonly ApplicationDbContext _context; 
        private const string CarritoSessionKey = "Carrito";

        public CarritoController(ILogger<CarritoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public IActionResult Index()
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<EventoViewModel>>(CarritoSessionKey) ?? new List<EventoViewModel>();
            return View(carrito);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito(EventoViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var carrito = HttpContext.Session.GetObjectFromJson<List<EventoViewModel>>(CarritoSessionKey) ?? new List<EventoViewModel>();
            carrito.Add(model);
            HttpContext.Session.SetObjectAsJson(CarritoSessionKey, carrito);

            // Crear un nuevo PedidoEvento
            var pedidoEvento = new PedidoEvento
            {
                IDCliente = model.vidcliente, 
                IDEvento = model.IDEvento,
                Detalle = model.Detalle,
                Cantidad = model.Cantidad,
                PrecioUnitario = model.PrecioUnitario,
                PrecioTotal = model.PrecioTotal,
                Fecha = DateTime.UtcNow 
            };

            // Guardar en la base de datos
            _context.DataPedidoEvento.Add(pedidoEvento);
            await _context.SaveChangesAsync(); 

            TempData["SuccessMessage"] = "Evento agregado al carrito de compras.";
            return RedirectToAction("Index", "Carrito");
        }

        public IActionResult EliminarDelCarrito(int idEvento)
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<EventoViewModel>>(CarritoSessionKey) ?? new List<EventoViewModel>();
            var eventoToRemove = carrito.FirstOrDefault(e => e.IDEvento == idEvento);
            if (eventoToRemove != null)
            {
                carrito.Remove(eventoToRemove);
                HttpContext.Session.SetObjectAsJson(CarritoSessionKey, carrito);
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
