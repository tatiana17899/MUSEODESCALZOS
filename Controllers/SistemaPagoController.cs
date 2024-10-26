using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MUSEODESCALZOS.Models;
using MUSEODESCALZOS.ViewModel;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using MUSEODESCALZOS.Data;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using MuseoDescalzos.Models;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using Newtonsoft.Json;

namespace MUSEODESCALZOS.Controllers
{
    public class SistemaPagoController : Controller
    {
        private readonly ILogger<SistemaPagoController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public SistemaPagoController(ILogger<SistemaPagoController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _configuration = configuration;

            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cliente = await _context.DataCliente.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cliente == null)
            {
                ModelState.AddModelError("", "Cliente no encontrado.");
                return View(new PagoViewModel());
            }

            var eventos = await _context.DataPedidoEvento
                .Where(p => p.IDCliente == cliente.IDCliente)
                .Include(e => e.Evento)
                .ToListAsync();

            decimal total = eventos.Sum(evento => evento.PrecioTotal > 0 ? evento.PrecioTotal : 0);
            var model = new PagoViewModel
            {
                IDCliente = cliente.IDCliente,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                TipoDoc = cliente.TipoDoc,
                NumDoc = cliente.NumDoc,
                ImagenUrl = eventos.FirstOrDefault()?.Evento.RutalImagen,
                Total = total,
                Eventos = eventos.Select(e => new EventoViewModel
                {
                    IDEvento = (int)e.IDEvento,
                    Detalle = e.Evento.Nombre,
                    Eventoo = e.Evento,
                    PrecioUnitario = e.PrecioUnitario,
                    Cantidad = e.Cantidad,
                    Fecha = e.Fecha,
                }).ToList()
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProcesarPago(PagoViewModel model)
        {
            var session = await CrearSesionStripe(model);

            if (session == null)
            {
                return BadRequest(new { message = "Error al crear la sesión de pago." });
            }

            var clienteExistente = await _context.DataCliente
                .FirstOrDefaultAsync(c => c.NumDoc == model.NumDoc && c.TipoDoc == model.TipoDoc);

            if (clienteExistente != null)
            {
                TempData["ClienteId"] = clienteExistente.IDCliente.ToString();
                // Aquí rediriges a Stripe
                return Ok(new { redirectUrl = session.Url }); // Redirige a la sesión de Stripe
            }
            else
            {
                var nuevoCliente = new Cliente
                {
                    Nombres = model.Nombres,
                    Apellidos = model.Apellidos,
                    TipoDoc = model.TipoDoc,
                    NumDoc = model.NumDoc,
                    Pais = model.Pais,
                    NumTarjeta = model.NumeroTarjeta
                };

                var clienteStripe = await CrearClienteStripe(model);
                nuevoCliente.StripeCustomerId = clienteStripe.Id;

                _context.DataCliente.Add(nuevoCliente);
                await _context.SaveChangesAsync();

                TempData["ClienteId"] = nuevoCliente.IDCliente.ToString();
                // Aquí rediriges a Stripe
                return Ok(new { redirectUrl = session.Url }); // Redirige a la sesión de Stripe
            }
        }


        private async Task<Session> CrearSesionStripe(PagoViewModel model)
        {
            try
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = "Compra de entradas",
                                },
                                UnitAmount = (long)(model.Total * 100),
                            },
                            Quantity = 1,
                        },
                    },
                    Mode = "payment",
                    SuccessUrl = Url.Action("Success", "SistemaPago", null, Request.Scheme),
                    CancelUrl = Url.Action("Cancel", "SistemaPago", null, Request.Scheme),
                };

                var service = new SessionService();
                return await service.CreateAsync(options);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la sesión de pago en Stripe.");
                return null; 
            }
        }
        private async Task<Customer> CrearClienteStripe(PagoViewModel model)
        {
            var customerOptions = new CustomerCreateOptions
            {
                Name = $"{model.Nombres} {model.Apellidos}",
                Email = model.Email, // Asegúrate de que el modelo tenga un campo para el email
                // Puedes agregar más campos según sea necesario
            };

            var customerService = new CustomerService();
            return await customerService.CreateAsync(customerOptions);
        }
        public IActionResult Success()
        {
            if (TempData["ClienteId"] != null)
            {
                long clienteId = long.Parse(TempData["ClienteId"].ToString());

                return RedirectToAction("ConfirmacionTarjeta", new { id = clienteId });
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult ConfirmacionTarjeta(long? id)
        {
            if (!id.HasValue)
            {
                id = TempData["ClienteId"] != null ? long.Parse(TempData["ClienteId"].ToString()) : (long?)null;
            }

            if (!id.HasValue)
            {
                return RedirectToAction("Index"); // Redirige a la vista de inicio si no se encuentra el ID
            }

            var cliente = _context.DataCliente.Find(id.Value);
            if (cliente == null)
            {
                return RedirectToAction("Index"); // Redirige si el cliente no existe
            }

            var eventosJson = TempData["Eventos"]?.ToString();
            var total = TempData["Total"] != null ? (decimal)TempData["Total"] : 0;

            if (eventosJson == null)
            {
                return RedirectToAction("Index"); // Redirige si no hay eventos
            }

            var eventos = JsonConvert.DeserializeObject<List<EventoViewModel>>(eventosJson);

            var confirmacionModel = new ConfirmacionViewModel
            {
                Cliente = cliente,
                Eventos = eventos,
                Total = total
            };

            return View(confirmacionModel);
        }



        [HttpPost]
        public async Task<IActionResult> GenerarPDF(int idCliente)
        {
            var cliente = await _context.DataCliente.FirstOrDefaultAsync(c => c.IDCliente == idCliente);
            var eventos = await _context.DataPedidoEvento
                .Include(e => e.Evento) // Asegúrate de incluir el evento relacionado
                .Where(p => p.IDCliente == idCliente)
                .ToListAsync();

            if (cliente == null || eventos == null || !eventos.Any())
            {
                return NotFound(); 
            }

            // Crear el modelo que se pasará a la vista
            var model = new ConfirmacionPagoViewModel
            {
                Cliente = cliente,
                Eventos = eventos
            };

            // Generar el PDF a partir de la vista
            var pdf = new ViewAsPdf("~/Views/SistemaPago/ConfirmacionPago.cshtml", model)
            {
                FileName = "ConfirmacionPago.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
            };


            var pdfBytes = await pdf.BuildFile(ControllerContext);
            return File(pdfBytes, "application/pdf", "ConfirmacionPago.pdf");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
