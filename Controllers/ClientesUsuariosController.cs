using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUSEODESCALZOS.Data;
using MuseoDescalzos.Models;
using System.Threading.Tasks;

namespace MuseoDescalzos.Controllers
{
    public class ClientesUsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesUsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Clientes
        public async Task<IActionResult> Index()
        {
            var model = new ClientesUsuarios
            {
                Clientes = await _context.DataCliente.ToListAsync()
            };

            // Renderiza la vista en Views/Admin/ClienteUsuarioCRUD.cshtml
            return View("~/Views/Admin/ClienteUsuarioCRUD.cshtml", model);
        }
    }
}
