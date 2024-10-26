using Microsoft.AspNetCore.Mvc;
using MuseoDescalzos.Models;
using MUSEODESCALZOS.Data;
using MUSEODESCALZOS.Models;
using System.Collections.Generic;
using System.Linq;

namespace MUSEODESCALZOS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventoApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/evento
        [HttpGet]
        public ActionResult<IEnumerable<Evento>> GetEventos()
        {
            var eventos = _context.DataEvento.ToList();
            return Ok(eventos);
        }

        // GET: api/evento/{id}
        [HttpGet("{id}")]
        public ActionResult<Evento> GetEvento(long id)
        {
            var evento = _context.DataEvento.Find(id);

            if (evento == null)
            {
                return NotFound(); // Maneja el caso donde no se encuentra el evento
            }

            return Ok(evento);
        }

        // POST: api/evento
        [HttpPost]
        public ActionResult<Evento> CreateEvento(Evento nuevoEvento)
        {
            _context.DataEvento.Add(nuevoEvento);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEvento), new { id = nuevoEvento.IDEvento }, nuevoEvento);
        }

        // PUT: api/evento/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEvento(long id, Evento eventoActualizado)
        {
            if (id != eventoActualizado.IDEvento)
            {
                return BadRequest(); // Código 400 Bad Request
            }

            var evento = _context.DataEvento.Find(id);
            if (evento == null)
            {
                return NotFound(); // Código 404 Not Found
            }

            // Actualiza las propiedades del evento
            evento.Nombre = eventoActualizado.Nombre;
            evento.Descripción = eventoActualizado.Descripción;
            evento.Fecha = eventoActualizado.Fecha;
            evento.Precio = eventoActualizado.Precio;
            evento.Capacidad = eventoActualizado.Capacidad;
            evento.NombreImagen = eventoActualizado.NombreImagen;
            evento.RutalImagen = eventoActualizado.RutalImagen;

            _context.DataEvento.Update(evento);
            _context.SaveChanges();

            return NoContent(); // Código 204 No Content
        }

        // DELETE: api/evento/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEvento(long id)
        {
            var evento = _context.DataEvento.Find(id);
            if (evento == null)
            {
                return NotFound(); // Código 404 Not Found
            }

            _context.DataEvento.Remove(evento);
            _context.SaveChanges();

            return NoContent(); // Código 204 No Content
        }
    }
}

