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
    public class EventoController : Controller
    {
        private readonly ILogger<EventoController> _logger;

        private readonly ApplicationDbContext _context;

        public EventoController(ApplicationDbContext context, ILogger<EventoController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var evento = from o in _context.DataEvento select o;
            var viewModel = new EventoViewModel
            {
                FormEvento = new Evento(),
                ListEvento = evento.ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var evento = _context.DataEvento.FirstOrDefault(gu => gu.IDEvento == id);
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
        public IActionResult GetGuiaById(int id)
        {
            var evento = _context.DataEvento.FirstOrDefault(gu => gu.IDEvento == id);
            if (evento == null)
            {
                return NotFound();
            }
            return Json(evento);
        }

        [HttpPost]
        public IActionResult Guardar(EventoViewModel viewModel)
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
                    NombreImagen = viewModel.FormEvento.NombreImagen,
                    RutalImagen = viewModel.FormEvento.RutalImagen
                };
                _context.DataEvento.Add(nuevoEvento);
            }
            else
            {
                var eventoExistente = _context.DataEvento.FirstOrDefault(g => g.IDEvento == viewModel.FormEvento.IDEvento);
                if (eventoExistente != null)
                {
                    eventoExistente.Nombre = viewModel.FormEvento.Nombre;
                    eventoExistente.Descripción = viewModel.FormEvento.Descripción;
                    eventoExistente.Fecha = viewModel.FormEvento.Fecha;
                    eventoExistente.Precio = viewModel.FormEvento.Precio;
                    eventoExistente.Capacidad = viewModel.FormEvento.Capacidad;
                    eventoExistente.NombreImagen = viewModel.FormEvento.NombreImagen;
                    eventoExistente.RutalImagen = viewModel.FormEvento.RutalImagen;

                    _context.DataEvento.Update(eventoExistente);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int id){
            var evento = _context.DataEvento.FirstOrDefault(gu => gu.IDEvento == id);
            if (evento != null){
                 _context.Remove(evento);
                _context.SaveChanges();
                TempData["Message"] = "Se ha eliminado la mascota.";
            }
            return RedirectToAction(nameof(Index));
        }

/*
        [HttpPost]
        public IActionResult AsignarTarea(long guiaId, string descripcion)
        {
            var guia = _context.DataGuía.FirstOrDefault(g => g.IDGuía == guiaId);
            if (guia != null)
            {
                var tarea = new Tarea
                {
                    GuíaID = guiaId,
                    Descripción = descripcion
                };
                _context.DataTareas.Add(tarea);
                _context.SaveChanges();
                TempData["Message"] = "Tarea asignada exitosamente.";
            }
            return RedirectToAction("Index");
        }
*/


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}