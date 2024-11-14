using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUSEODESCALZOS.Data;
using MuseoDescalzos.Models;
using System.Threading.Tasks;
using OfficeOpenXml;
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
        public async Task<IActionResult> ExportToExcel()
        {
            var clientes = await _context.DataCliente.ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Clientes");
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Nombres";
                worksheet.Cells[1, 3].Value = "Apellidos";
                worksheet.Cells[1, 4].Value = "Tipo de Documento";
                worksheet.Cells[1, 5].Value = "Número de Documento";
                worksheet.Cells[1, 6].Value = "País";

                for (int i = 0; i < clientes.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = clientes[i].IDCliente;
                    worksheet.Cells[i + 2, 2].Value = clientes[i].Nombres;
                    worksheet.Cells[i + 2, 3].Value = clientes[i].Apellidos;
                    worksheet.Cells[i + 2, 4].Value = clientes[i].TipoDoc;
                    worksheet.Cells[i + 2, 5].Value = clientes[i].NumDoc;
                    worksheet.Cells[i + 2, 6].Value = clientes[i].Pais;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                var fileName = "Clientes.xlsx";
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
