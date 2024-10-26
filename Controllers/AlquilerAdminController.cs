using MUSEODESCALZOS.Data;
using MUSEODESCALZOS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuseoDescalzos.Models;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;


namespace MUSEODESCALZOS.Controllers
{
    public class AlquilerAdminController : Controller
    {
        private ApplicationDbContext _context;

        public AlquilerAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var alquileres = from o in _context.DataAlquiler select o;
            var viewModel = new AlquilerViewModel
            {
                FormAlquiler = new Alquiler(),
                ListAlquiler = alquileres.ToList(),
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
                .Include(al=> al.Imagenes).ToList() // Esto asegura que se incluyan las imágenes relacionadas
                .FirstOrDefault(al => al.IDAlquileres == id);
            if (alquiler == null)
            {
                return NotFound();
            }
            return Json(alquiler);
        }


        [HttpPost]
        public async Task<IActionResult> Guardar(AlquilerViewModel viewModel)
        {
            if (viewModel.FormAlquiler.IDAlquileres == 0)
            {
                var nuevoAlquiler = new Alquiler
                {
                    Titulo = viewModel.FormAlquiler.Titulo,
                    Caracteristicas = viewModel.FormAlquiler.Caracteristicas,
                    Capacidad = viewModel.FormAlquiler.Capacidad,
                    Imagenes = viewModel.FormAlquiler.Imagenes,
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
                    alquilerExistente.Imagenes = viewModel.FormAlquiler.Imagenes;
                    alquilerExistente.Disponible = viewModel.FormAlquiler.Disponible;
                    _context.DataAlquiler.Update(alquilerExistente);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
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
        public IActionResult SetAlquilerId(long alquilerId)
        {
            ViewBag.AlquilerId = alquilerId;
            return Ok();
        }

        [HttpPost]
        public IActionResult GuardarAlquiler(AlquilerViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Asumiendo que tienes un servicio para guardar la información
                Alquiler nuevoAlquiler = new Alquiler
                {
                    // Asignar las propiedades que vengan del ViewModel al nuevo objeto Alquiler
                    Titulo = model.Alquiler,
                    Hora_Disponible = model.HoraInicio, // O ajusta según tu lógica
                    Capacidad = model.CantPersona,
                    // Asigna otras propiedades necesarias
                };

                // Guardar en la base de datos
                // dbContext.Alquileres.Add(nuevoAlquiler);
                // dbContext.SaveChanges();

                // Redirigir a una página de éxito o a la lista de alquileres
                return RedirectToAction("Index");
            }

            // Si el modelo no es válido, vuelve a mostrar la vista con los errores
            return View(model);
        }



    }
}