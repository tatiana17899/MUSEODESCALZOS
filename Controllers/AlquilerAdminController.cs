using MUSEODESCALZOS.Data;
using MUSEODESCALZOS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuseoDescalzos.Models;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Firebase.Storage;


namespace MUSEODESCALZOS.Controllers
{
    public class AlquilerAdminController : Controller
    {
        private ApplicationDbContext _context;

        public AlquilerAdminController(ApplicationDbContext context)
        {
            _context = context;
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile("firebase-credentials.json")
                });
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            var alquileres = _context.DataAlquiler
                                    .Include(a => a.Imagenes) // Incluir las imágenes asociadas
                                    .ToList();
            var reservas=_context.DataPedidoAlquiler
                                .Include(a=>a.Cliente)
                                .Include(a=>a.Alquiler)
                                .ToList();
            var viewModel = new AlquilerViewModel
            {
                FormAlquiler = new Alquiler(),
                ListAlquiler = alquileres,
                ListPedidoAlquiler=reservas
            };
            
            ViewBag.AlquilerId = 0;
            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


        [HttpGet]
        public IActionResult Editar(int id)
        {
            var alquiler = _context.DataAlquiler.FirstOrDefault(al => al.IDAlquileres == id);
            if (alquiler == null)
            {
                return NotFound();
            }
            var viewModel = new AlquilerViewModel
            {
                FormAlquiler = alquiler,
                ListAlquiler = _context.DataAlquiler.ToList()
            };
            return View("Index", viewModel);
        }
        [HttpGet]
        public IActionResult GetAlquilerById(int id)
        {
            var alquiler = _context.DataAlquiler
                //.Include(al=> al.Imagenes) // Esto asegura que se incluyan las imágenes relacionadas
                .FirstOrDefault(al => al.IDAlquileres == id);
            if (alquiler == null)
            {
                return NotFound();
            }
            return Json(alquiler);
        }
    [HttpGet]
    public IActionResult GetReservaById(long id)
    {
        var reserva = _context.DataPedidoAlquiler
                            .Include(r => r.Cliente) // Si necesitas información del cliente
                            .Include(r => r.Alquiler) // Si necesitas información del alquiler
                            .FirstOrDefault(r => r.IDPedidoAlq == id);

        if (reserva == null)
        {
            return NotFound();
        }

        var helperModel = new ReservaAlquilerHelperModel
        {
            IDReserva = reserva.IDPedidoAlq,
            IDAlquileres = reserva.IDAlquiler,
            NumeroDocumento = reserva.Cliente.NumDoc,
            Cantidad = reserva.CantPersona,
            Fecha = reserva.Fecha.ToString("yyyy-MM-dd"),
            HoraInicio = reserva.Hora_Inicio.ToString(@"hh\:mm"),
            HoraFin = reserva.Hora_Fin.ToString(@"hh\:mm"),
            Precio = reserva.PrecioTotal
        };

        return Json(helperModel);
    }




        [HttpPost]
        public async Task<IActionResult> Guardar(AlquilerViewModel viewModel)
        {
            Console.WriteLine("Alquiler", viewModel.FormAlquiler.ImagenesFiles);
                var urlsImagenes = new List<Imagen_Alquiler>();
    
            if (viewModel.FormAlquiler.ImagenesFiles != null && viewModel.FormAlquiler.ImagenesFiles.Any())
            {
                foreach (var imagenFile in viewModel.FormAlquiler.ImagenesFiles)
                {
                    var urlImagen = await SubirImagenFirebase(imagenFile);
                    urlsImagenes.Add(new Imagen_Alquiler { Rutalmagen = urlImagen });
                }
            }

            
            if (viewModel.FormAlquiler.IDAlquileres == 0)
            {
                var nuevoAlquiler = new Alquiler
                {
                    Titulo = viewModel.FormAlquiler.Titulo,
                    Caracteristicas = viewModel.FormAlquiler.Caracteristicas,
                    Capacidad = viewModel.FormAlquiler.Capacidad,
                    Imagenes = urlsImagenes,
                    Disponible = viewModel.FormAlquiler.Disponible,

                };
                _context.DataAlquiler.Add(nuevoAlquiler);
                await _context.SaveChangesAsync();
            }
            else
            {
                var alquilerExistente = _context.DataAlquiler.FirstOrDefault(a => a.IDAlquileres == viewModel.FormAlquiler.IDAlquileres);
                if (alquilerExistente != null)
                {
                    alquilerExistente.Titulo = viewModel.FormAlquiler.Titulo;
                    alquilerExistente.Caracteristicas = viewModel.FormAlquiler.Caracteristicas;
                    alquilerExistente.Capacidad = viewModel.FormAlquiler.Capacidad;
                    alquilerExistente.Disponible = viewModel.FormAlquiler.Disponible;
                    if (urlsImagenes.Any()) // Solo actualiza si se han añadido nuevas imágenes
                    {
                        alquilerExistente.Imagenes = urlsImagenes;
                    }
                    _context.DataAlquiler.Update(alquilerExistente);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Reservar(ReservaAlquilerViewModel reserva)
        {
            Console.WriteLine("Ingreso a reservar");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("MODELO INVALIDO");
                return RedirectToAction("Index");
            }

            try
            {
                var cliente = _context.DataCliente.FirstOrDefault(a => a.NumDoc == reserva.NumeroDocumento);
                if (cliente == null)
                {
                    Console.WriteLine("Cliente no encontrado");
                    ModelState.AddModelError("", "Cliente no encontrado.");
                    return RedirectToAction("Index");
                }

                // Combina la fecha con la hora de inicio y fin
                DateTime fechaInicio = reserva.Fecha.Date + reserva.HoraInicio;
                DateTime fechaFin = reserva.Fecha.Date + reserva.HoraFin;

                Console.WriteLine($"Fecha Inicio: {fechaInicio}, Fecha Fin: {fechaFin}");

                if (reserva.IDReserva > 0) // Verifica si existe un idReserva
                {
                    // Editar reserva existente
                    var existingReserva = _context.DataPedidoAlquiler.FirstOrDefault(r => r.IDPedidoAlq == reserva.IDReserva);
                    if (existingReserva != null)
                    {
                        existingReserva.IDCliente = cliente.IDCliente;
                        existingReserva.IDAlquiler = reserva.IDAlquileres;
                        existingReserva.Fecha = reserva.Fecha.ToUniversalTime();
                        existingReserva.Hora_Inicio = fechaInicio.ToUniversalTime();
                        existingReserva.Hora_Fin = fechaFin.ToUniversalTime();
                        existingReserva.CantPersona = reserva.Cantidad;
                        existingReserva.PrecioTotal = reserva.Precio;

                        // Actualiza la reserva en la base de datos
                        _context.DataPedidoAlquiler.Update(existingReserva);
                        _context.SaveChanges();

                        Console.WriteLine("Reserva actualizada con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("Reserva no encontrada para editar.");
                        ModelState.AddModelError("", "Reserva no encontrada.");
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Crear nueva reserva
                    var newPedidoAlquiler = new PedidoAlquiler
                    {
                        IDCliente = cliente.IDCliente,
                        IDAlquiler = reserva.IDAlquileres,
                        Fecha = reserva.Fecha.ToUniversalTime(),
                        Hora_Inicio = fechaInicio.ToUniversalTime(),
                        Hora_Fin = fechaFin.ToUniversalTime(),
                        CantPersona = reserva.Cantidad,
                        PrecioTotal = reserva.Precio
                    };

                    // Guarda el nuevo pedido en la base de datos
                    _context.DataPedidoAlquiler.Add(newPedidoAlquiler);
                    _context.SaveChanges();

                    Console.WriteLine("Reserva creada con éxito.");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Registra el error si es necesario
                Console.WriteLine($"Error: {ex.Message}");
                ModelState.AddModelError("", $"Ocurrió un error al reservar: {ex.Message}");
                return RedirectToAction("Index"); // Considera redirigir a una vista que pueda mostrar errores
            }
        }



        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var alquiler = _context.DataAlquiler.FirstOrDefault(al => al.IDAlquileres == id);
            if (alquiler != null)
            {
                _context.Remove(alquiler);
                _context.SaveChanges();
                TempData["Message"] = "Se ha eliminado el alquiler.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult EliminarReserva(int id)
        {
            var alquiler = _context.DataPedidoAlquiler.FirstOrDefault(al => al.IDPedidoAlq == id);
            if (alquiler != null)
            {
                _context.Remove(alquiler);
                _context.SaveChanges();
                TempData["Message"] = "Se ha eliminado el alquiler.";
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult SetAlquilerId(long alquilerId)
        {
            ViewBag.AlquilerId = alquilerId;
            return Ok();
        }
        private async Task<string> SubirImagenFirebase(IFormFile imagenFile)
        {
            var storage = new FirebaseStorage("webmuseodescalzos.appspot.com");
            var stream = imagenFile.OpenReadStream();
            var fileName = $"{Guid.NewGuid()}.jpg";

            return await storage
                .Child("ListaAlquileres")
                .Child(fileName)
                .PutAsync(stream);
        }

    }
}