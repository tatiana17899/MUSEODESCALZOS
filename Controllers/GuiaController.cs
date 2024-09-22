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
namespace MUSEO_DE_LOS_DESCALZOS.Controllers
{
    public class GuiaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GuiaController> _logger;

        public GuiaController(ApplicationDbContext context, ILogger<GuiaController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var guias= from o in _context.DataGuía select o;
            var viewModel = new GuiaViewModel{
                FormGuia = new Guía(),
                ListGuía = guias.ToList()
            };

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var guia= _context.DataGuía.FirstOrDefault(gu => gu.IDGuía == id);
            if (guia == null){
                return NotFound();
            }
            var viewModel = new GuiaViewModel{
                FormGuia = guia,
                ListGuía= _context.DataGuía.ToList()
            };
            return View("Index", viewModel);
        }
        [HttpGet]
        public IActionResult GetGuiaById(int id)
        {
            var guia = _context.DataGuía.FirstOrDefault(gu => gu.IDGuía == id);
            if (guia == null)
            {
                return NotFound();
            }
            return Json(guia);
        }
        [HttpPost]
        public IActionResult Guardar(GuiaViewModel viewModel)
        {
            if(viewModel.FormGuia.IDGuía == 0){
                var nuevaGuia = new Guía
                {
                    Nombres = viewModel.FormGuia.Nombres,
                    Apellidos = viewModel.FormGuia.Apellidos,
                    Email = viewModel.FormGuia.Email,
                    TipPago = viewModel.FormGuia.TipPago,
                    Sueldo = viewModel.FormGuia.Sueldo,
                    PedidoVisita = viewModel.FormGuia.PedidoVisita,
                    Actividades = viewModel.FormGuia.Actividades
                };
                _context.DataGuía.Add(nuevaGuia);
            }
             else
            {
                var guiaExistente = _context.DataGuía.FirstOrDefault(g => g.IDGuía == viewModel.FormGuia.IDGuía);
                if (guiaExistente != null)
                {
                    guiaExistente.Nombres = viewModel.FormGuia.Nombres;
                    guiaExistente.Apellidos = viewModel.FormGuia.Apellidos;
                    guiaExistente.Email = viewModel.FormGuia.Email;
                    guiaExistente.TipPago = viewModel.FormGuia.TipPago;
                    guiaExistente.Sueldo = viewModel.FormGuia.Sueldo;
                    guiaExistente.PedidoVisita = viewModel.FormGuia.PedidoVisita;
                    guiaExistente.Actividades = viewModel.FormGuia.Actividades;

                    _context.DataGuía.Update(guiaExistente);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Eliminar(int id){
            var guia = _context.DataGuía.FirstOrDefault(gu => gu.IDGuía == id);
            if (guia != null){
                 _context.Remove(guia);
                _context.SaveChanges();
                TempData["Message"] = "Se ha eliminado la mascota.";
            }
            return RedirectToAction(nameof(Index));
        }
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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}