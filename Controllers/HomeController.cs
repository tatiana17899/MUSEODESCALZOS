using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MuseoDescalzos.Models;
using MUSEODESCALZOS.Data;
using MUSEODESCALZOS.Models;
namespace MUSEODESCALZOS.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public IActionResult Index()
    {
        List<Evento> eventos = _context.DataEvento.ToList(); 

        return View(eventos);
    }

    public IActionResult Visita()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    
    }
}
