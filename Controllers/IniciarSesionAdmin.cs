using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using MUSEODESCALZOS.Data;

namespace MUSEO_DE_LOS_DESCALZOS.Controllers
{
    public class IniciarSesionAdmin : Controller
    {
        private readonly ILogger<IniciarSesionAdmin> _logger;
        private readonly ApplicationDbContext _context;

        public IniciarSesionAdmin(ApplicationDbContext context, ILogger<IniciarSesionAdmin> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(IniciarSesionAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var administrador = _context.DataAdministrador.FirstOrDefault(a => a.Email == model.Email && a.Contraseña == model.Contraseña);

                if (administrador != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, administrador.Email),
                        new Claim("FullName", administrador.Nombres + " " + administrador.Apellidos),
                        new Claim(ClaimTypes.Role, "Administrador")
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Dashboard") }); // Retorna un JSON si es correcto
                }
                
                // Si las credenciales son incorrectas
                return Json(new { success = false, message = "Email o contraseña incorrectos." });
            }

            return View("Index", model); 
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "IniciarSesionAdmin");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}