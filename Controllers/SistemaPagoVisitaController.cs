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
    public class SistemaPagoVisitaController : Controller
    {
        private readonly ILogger<SistemaPagoVisitaController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public SistemaPagoVisitaController(ILogger<SistemaPagoVisitaController> logger, ApplicationDbContext context, 
            UserManager<IdentityUser> userManager, IConfiguration configuration)
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
                return View(new PagoVisitaViewModel());
            }

            var pedidosVisitas = await _context.DataPedidoVisita
                .Where(p => p.IDCliente == cliente.IDCliente)
                .Include(v => v.Cliente)
                .Include(v => v.Guía)
                .ToListAsync();

            decimal total = pedidosVisitas.Sum(p => p.PrecioTotal);

            var model = new PagoVisitaViewModel
            {
                IDCliente = cliente.IDCliente,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                TipoDoc = cliente.TipoDoc,
                NumDoc = cliente.NumDoc,
                Total = total,
                PedidosVisitas = pedidosVisitas,
                Visitas = pedidosVisitas.Select(p => new ReservarVisitaViewModel
                {
                    Fecha = p.Fecha,
                    Hora = p.Fecha.ToString("HH:mm"),
                    GuiaID = p.IDGuía,
                    PrecioTotal = p.PrecioTotal
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProcesarPago(PagoVisitaViewModel model)
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



        private async Task<Session> CrearSesionStripe(PagoVisitaViewModel model)
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
                                    Name = "Reserva de visitas al museo",
                                },
                                UnitAmount = (long)(model.Total * 100),
                            },
                            Quantity = 1,
                        },
                    },
                    Mode = "payment",
                    SuccessUrl = Url.Action("Success", "SistemaPagoVisita", null, Request.Scheme),
                    CancelUrl = Url.Action("Cancel", "SistemaPagoVisita", null, Request.Scheme),
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

        private async Task<Customer> CrearClienteStripe(PagoVisitaViewModel model)
        {
            var customerOptions = new CustomerCreateOptions
            {
                Name = $"{model.Nombres} {model.Apellidos}",
                Email = model.Email,
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
        public async Task<IActionResult> ConfirmacionTarjeta(long? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var cliente = await _context.DataCliente.FindAsync(id.Value);
            if (cliente == null)
            {
                return RedirectToAction("Index");
            }

            var visitasJson = TempData["Visitas"]?.ToString();
            var total = TempData["Total"] != null ? Convert.ToDecimal(TempData["Total"]) : 0m;

            if (string.IsNullOrEmpty(visitasJson))
            {
                return RedirectToAction("Index");
            }

            var visitas = JsonConvert.DeserializeObject<List<ReservarVisitaViewModel>>(visitasJson);
            
            var model = new ConfirmacionVisitaViewModel
            {
                Cliente = cliente,
                Visitas = visitas,
                Total = total
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GenerarPDF(long idCliente)
        {
            var cliente = await _context.DataCliente.FindAsync(idCliente);
            var pedidosVisitas = await _context.DataPedidoVisita
                .Where(p => p.IDCliente == idCliente)
                .ToListAsync();

            if (cliente == null || !pedidosVisitas.Any())
            {
                return NotFound();
            }

            // Ya no necesitas crear una lista de ReservarVisitaViewModel
            // porque vas a usar directamente pedidosVisitas

            var model = new ConfirmacionVisitaPagoViewModel
            {
                Cliente = cliente,
                visita = pedidosVisitas  // Asigna directamente la lista de pedidos
            };

            var pdf = new ViewAsPdf("ConfirmacionPago", model)
            {
                FileName = $"ConfirmacionPago_{idCliente}.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
            };

            var pdfBytes = await pdf.BuildFile(ControllerContext);
            return File(pdfBytes, "application/pdf", pdf.FileName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}