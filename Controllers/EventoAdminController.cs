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
        }

        public IActionResult Index()
        {
            var eventos = from o in _context.DataEvento select o;
            var viewModel = new EventoViewModel
            {
                FormEvento = new Evento(),
                ListEvento = eventos.ToList(),
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
        public IActionResult GetEventoFaById(int id)
        {
            var evento = _context.DataEvento.FirstOrDefault(ev => ev.IDEvento == id);
            if (evento == null)
            {
                return NotFound();
            }
            return Json(evento);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(EventoViewModel viewModel)
        {
            if (viewModel.FormEvento.IDEvento == 0)
            {
                var nuevoEvento = new Evento
                {
                    Nombre = viewModel.FormEvento.Nombre,
                    Descripción = viewModel.FormEvento.Descripción,
                    Fecha = viewModel.FormEvento.Fecha,
                    Precio = viewModel.FormEvento.Precio,
                    Capacidad = viewModel.FormEvento.Capacidad,
                    RutalImagen = viewModel.FormEvento.RutalImagen,

                };
                _context.DataEvento.Add(nuevoEvento);
                await _context.SaveChangesAsync();
            }
            else
            {
                var eventoExistente = _context.DataEvento.FirstOrDefault(e => e.IDEvento == viewModel.FormEvento.IDEvento);
                if (eventoExistente != null)
                {
                    eventoExistente.Nombre = viewModel.FormEvento.Nombre;
                    eventoExistente.Descripción = viewModel.FormEvento.Descripción;
                    eventoExistente.Fecha = viewModel.FormEvento.Fecha;
                    eventoExistente.Precio = viewModel.FormEvento.Precio;
                    eventoExistente.Capacidad = viewModel.FormEvento.Capacidad;
                    eventoExistente.RutalImagen = viewModel.FormEvento.RutalImagen;
                    _context.DataEvento.Update(eventoExistente);
                }
            }

            _context.SaveChanges();
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

    }
}