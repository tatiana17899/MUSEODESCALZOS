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
                return BadRequest(new { message = "Error al crear la sesión de pago." }); // Devuelve un error JSON
            }

            var clienteExistente = await _context.DataCliente
                .FirstOrDefaultAsync(c => c.NumDoc == model.NumDoc && c.TipoDoc == model.TipoDoc);

            if (clienteExistente != null)
            {
                var confirmacionUrl = Url.Action("ConfirmacionTarjeta", new { id = clienteExistente.IDCliente });
                return Ok(new { redirectUrl = confirmacionUrl });
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
                _context.DataCliente.Add(nuevoCliente);
                await _context.SaveChangesAsync(); 

                var confirmacionUrl = Url.Action("ConfirmacionTarjeta", new { id = nuevoCliente.IDCliente });
                return Ok(new { redirectUrl = confirmacionUrl }); 
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



        [HttpGet]
        public IActionResult ConfirmacionTarjeta(long id)
        {
            Console.WriteLine($"Buscando cliente con ID: {id}");
            var cliente = _context.DataCliente.Find(id);
            if (cliente == null)
            {
                Console.WriteLine($"Cliente no encontrado con ID: {id}");
                return RedirectToAction("Index"); 
            }

            var eventosJson = TempData["Eventos"]?.ToString();
            var total = TempData["Total"] != null ? (decimal)TempData["Total"] : 0;

            if (eventosJson == null)
            {
                Console.WriteLine("No se encontraron eventos en TempData.");
                return RedirectToAction("Index"); 
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
        public IActionResult GenerarPDF(int idCliente)
        {
            var cliente = _context.DataCliente.FirstOrDefault(c => c.IDCliente == idCliente);
            var eventos = _context.DataPedidoEvento
                .Where(p => p.IDCliente == idCliente)
                .ToList();

            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph("Confirmación de Pago").SetFontSize(20));

                document.Add(new Paragraph($"Nombres: {cliente.Nombres}"));
                document.Add(new Paragraph($"Apellidos: {cliente.Apellidos}"));
                document.Add(new Paragraph($"Número de Documento: {cliente.NumDoc}"));

                document.Add(new Paragraph("Eventos Comprados:").SetBold());

                foreach (var evento in eventos)
                {
                    document.Add(new Paragraph($"{evento.Evento.Nombre} - Cantidad: {evento.Cantidad}, Total: {evento.PrecioTotal}"));
                }

                document.Close();
                return File(stream.ToArray(), "application/pdf", "ConfirmacionPago.pdf");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
