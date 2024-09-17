using MUSEODESCALZOS.Data;
using MUSEODESCALZOS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuseoDescalzos.Models;


namespace MUSEODESCALZOS.Controllers
{
    public class AlquilerController : Controller
    {
        private ApplicationDbContext _context;

        public AlquilerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var alquiler = await _context.Alquiler.ToListAsync();
            return View(alquiler);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        /*Creando funciones*/
        //Crear

        [HttpGet]
        public IActionResult CrearAlquiler()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearAlquiler(Alquiler alquiler)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(alquiler);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Algo salió mal{ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, $"Algo salió mal modelo inválido");
            return View(alquiler);
        }

        [HttpGet]
        public async Task<IActionResult> EditarAlquiler(int id)
        {
            var exist = await _context.Alquiler.Where(x => x.IdAlquiler == id).FirstOrDefaultAsync();
            return View(exist);
        }

        [HttpPost]
        public async Task<IActionResult> EditarAlquiler(Alquiler alquiler)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _context.Alquiler.Where(x => x.IdAlquiler == alquiler.IdAlquiler).FirstOrDefault();
                    if (exist != null)
                    {
                        exist.Titulo = alquiler.Titulo;
                        exist.Descripcion = alquiler.Descripcion;
                        exist.Capacidad = alquiler.Capacidad;
                        exist.Caracteristicas = alquiler.Caracteristicas;
                        exist.Hora_Disponible = alquiler.Hora_Disponible;
                        exist.Disponible = alquiler.Disponible;
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Algo salió mal{ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, $"Algo salió mal modelo inválido");
            return View(alquiler);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarAlquiler(int id)
        {
            var exist = await _context.Alquiler.Where(x => x.IdAlquiler == id).FirstOrDefaultAsync();
            return View(exist);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAlquiler(Alquiler alquiler)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _context.Alquiler.Where(x => x.IdAlquiler == alquiler.IdAlquiler).FirstOrDefault();
                    if (exist != null)
                    {
                        _context.Remove(exist);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Algo salió mal{ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, $"Algo salió mal modelo inválido");
            return View(alquiler);
        }

        

    }
}