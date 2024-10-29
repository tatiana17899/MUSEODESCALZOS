using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEODESCALZOS.Data;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using MuseoDescalzos.Models;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Firebase.Storage;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace MUSEODESCALZOS.Controllers
{
    public class EventoAdminController : Controller
    {
        private readonly ILogger<EventoAdminController> _logger;
        private readonly ApplicationDbContext _context;

        public EventoAdminController(ApplicationDbContext context, ILogger<EventoAdminController> logger)
        {
            _logger = logger;
            _context = context;
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile("firebase-credentials.json")
                });
            }
        }

        public IActionResult Index()
        {
            var eventos = from o in _context.DataEvento select o;
            var guias = from o in _context.DataGuía select o;

            var viewModel = new EventoViewModel
            {
                FormEvento = new Evento(),
                ListEvento = eventos.ToList(),
                ListGuia= guias.ToList(),
                ListActividades = new List<Actividades>() // Inicializa la lista de actividades

            };
            ViewBag.EventoId = 0;
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
            var evento = _context.DataEvento.FirstOrDefault(ev => ev.IDEvento == id);
            if (evento == null)
            {
                return NotFound();
            }
            var viewModel = new EventoViewModel
            {
                FormEvento = evento,
                ListEvento = _context.DataEvento.ToList()
            };
            
            return View("Index", viewModel);
        }

        [HttpGet]
        public IActionResult GetEventoById(int id)
        {
            // Recuperar el evento desde la base de datos
            var evento = _context.DataEvento
                .Include(ev => ev.Actividades) // Incluir actividades
                .FirstOrDefault(ev => ev.IDEvento == id);

            // Verificar si el evento existe
            if (evento == null)
            {
                return NotFound();
            }

            // Recuperar las actividades desde la base de datos, incluyendo la guía
            var actividades = _context.DataActividades
                .Include(ac => ac.Guía) // Incluir la guía
                .Where(ac => ac.IDEvento == evento.IDEvento) // Filtrar por el evento actual
                .ToList();

            // Construir el modelo de vista
            var viewModel = new EventoViewModel
            {
                FormEvento = new Evento
                {
                    IDEvento = evento.IDEvento,
                    Nombre = evento.Nombre,
                    Descripción = evento.Descripción,
                    Capacidad = evento.Capacidad,
                    Precio = evento.Precio,
                    Fecha = evento.Fecha.ToUniversalTime(),
                    // Omitir la propiedad que causa el ciclo
                },
                ListActividades = actividades.Select(a => 
                    new Actividades
                    {   
                        IDActividades = a.IDActividades,
                        Actividad = a.Actividad,
                        Hora_Inicial = a.Hora_Inicial.ToUniversalTime(),
                        Hora_Final = a.Hora_Final.ToUniversalTime(),
                        Guía = new Guía
                        {
                            IDGuía = a.Guía.IDGuía, // Asegúrate de que esta propiedad exista
                            Nombres = a.Guía.Nombres,
                            Apellidos = a.Guía.Apellidos
                        }
                    }).ToList()
            };

            return Json(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(EventoViewModel viewModel)
        {
            string viewModelJson = JsonSerializer.Serialize(viewModel, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine("Evento en formato JSON:");
            Console.WriteLine(viewModelJson);

            // Imprimir el nombre del evento y cada actividad
            Console.WriteLine($"Nombre del Evento: {viewModel.FormEvento.Nombre}");
            foreach (var actividad in viewModel.ListActividades)
            {
                Console.WriteLine($"Actividad: {JsonSerializer.Serialize(actividad, new JsonSerializerOptions { WriteIndented = true })}");
            }

            // Verificar si hay un ID de evento
            if (viewModel.FormEvento.IDEvento == 0)
            {
                // Crear nuevo evento
                if (viewModel.FormEvento.ImagenFile == null || viewModel.FormEvento.ImagenFile.Length == 0)
                {
                    ModelState.AddModelError("ImagenFile", "La imagen es requerida para crear un nuevo evento.");
                    return View(viewModel); // Devuelve a la vista con el error
                }

                var urlImagen = await SubirImagenFirebase(viewModel.FormEvento.ImagenFile);
                var nuevoEvento = new Evento
                {
                    Nombre = viewModel.FormEvento.Nombre,
                    Descripción = viewModel.FormEvento.Descripción,
                    Fecha = viewModel.FormEvento.Fecha.ToUniversalTime(),
                    Precio = viewModel.FormEvento.Precio,
                    Capacidad = viewModel.FormEvento.Capacidad,
                    RutalImagen = urlImagen,
                    Actividades = new List<Actividades>() // Inicializar la lista de actividades
                };

                // Añadir actividades al nuevo evento
                foreach (var actividad in viewModel.ListActividades)
                {
                    var guiaData = _context.DataGuía.FirstOrDefault(g => g.IDGuía == actividad.Guía.IDGuía);
                    if (guiaData != null)
                    {
                        var nuevaActividad = new Actividades
                        {
                            Actividad = actividad.Actividad,
                            Hora_Inicial = actividad.Hora_Inicial.ToUniversalTime(),
                            Hora_Final = actividad.Hora_Final.ToUniversalTime(),
                            Guía = guiaData // Asigna el guía existente
                        };
                        nuevoEvento.Actividades.Add(nuevaActividad);
                    }
                }

                _context.DataEvento.Add(nuevoEvento);
            }
            else
            {
                // Actualizar evento existente
                var eventoExistente = await _context.DataEvento.Include(e => e.Actividades)
                    .FirstOrDefaultAsync(e => e.IDEvento == viewModel.FormEvento.IDEvento);
                if (eventoExistente != null)
                {
                    eventoExistente.Nombre = viewModel.FormEvento.Nombre;
                    eventoExistente.Descripción = viewModel.FormEvento.Descripción;
                    eventoExistente.Fecha = viewModel.FormEvento.Fecha.ToUniversalTime();
                    eventoExistente.Precio = viewModel.FormEvento.Precio;
                    eventoExistente.Capacidad = viewModel.FormEvento.Capacidad;

                    // Solo subir nueva imagen si se ha seleccionado una
                    if (viewModel.FormEvento.ImagenFile != null && viewModel.FormEvento.ImagenFile.Length > 0)
                    {
                        var urlImagen = await SubirImagenFirebase(viewModel.FormEvento.ImagenFile);
                        eventoExistente.RutalImagen = urlImagen; // Actualizar solo si hay nueva imagen
                    }

                    // Actualizar las actividades
                    eventoExistente.Actividades.Clear(); // Limpiar actividades actuales
                    foreach (var actividad in viewModel.ListActividades)
                    {
                        var guiaData = _context.DataGuía.FirstOrDefault(g => g.IDGuía == actividad.Guía.IDGuía);
                        if (guiaData != null)
                        {
                            var nuevaActividad = new Actividades
                            {
                                Actividad = actividad.Actividad,
                                Hora_Inicial = actividad.Hora_Inicial.ToUniversalTime(),
                                Hora_Final = actividad.Hora_Final.ToUniversalTime(),
                                Guía = guiaData // Asigna el guía existente
                            };
                            eventoExistente.Actividades.Add(nuevaActividad);
                        }
                    }

                    _context.DataEvento.Update(eventoExistente);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var evento = _context.DataEvento.FirstOrDefault(ev => ev.IDEvento == id);
            if (evento != null)
            {
                _context.Remove(evento);
                _context.SaveChanges();
                TempData["Message"] = "Se ha eliminado el evento.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SetEventoId(long eventoId)
        {
            ViewBag.EventoId = eventoId;
            return Ok();
        }
        private async Task<string> SubirImagenFirebase(IFormFile imagenFile)
        {
            var storage = new FirebaseStorage("webmuseodescalzos.appspot.com");
            var stream = imagenFile.OpenReadStream();
            var fileName = $"{Guid.NewGuid()}.jpg";

            return await storage
                .Child("ListaEventos")
                .Child(fileName)
                .PutAsync(stream);
        }
    }
}