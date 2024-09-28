/*using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using System.Net.Mail;
using MUSEODESCALZOS.Data;

namespace MUSEO_DE_LOS_DESCALZOS.Controllers
{
    public class PasswordResetAdminController : Controller
    {
        private readonly ILogger<PasswordResetAdminController> _logger;
        private readonly ApplicationDbContext _context;

        public PasswordResetAdminController(ApplicationDbContext context, ILogger<PasswordResetAdminController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendPasswordResetLink(string email)
        {
            var administrador = _context.DataAdministrador.FirstOrDefault(a => a.Email == email);

            if (administrador != null)
            {
                var token = Guid.NewGuid().ToString();

                administrador.PasswordResetToken = token;
                administrador.ResetTokenExpiration = DateTime.Now.AddHours(1);
                await _context.SaveChangesAsync();

                var resetLink = Url.Action("ResetPassword", "PasswordResetAdminController", new { token }, Request.Scheme);
                await SendEmailAsync(email, "Restablecer contraseña", $"Haz clic en este enlace para restablecer tu contraseña: {resetLink}");

                return Json(new { success = true, message = "Correo de restablecimiento de contraseña enviado." });
            }

            return Json(new { success = false, message = "El correo no está registrado." });
        }

        public IActionResult ResetPassword(string token)
        {
            var administrador = _context.DataAdministrador.FirstOrDefault(a => a.PasswordResetToken == token);

            if (administrador != null && administrador.ResetTokenExpiration > DateTime.Now)
            {
                return View(new ResetPasswordViewModel { Token = token });
            }

            return View("TokenExpired");
        }

        [HttpPost]
        public async Task<IActionResult> SetNewPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var administrador = _context.DataAdministrador.FirstOrDefault(a => a.PasswordResetToken == model.Token);

                if (administrador != null && administrador.ResetTokenExpiration > DateTime.Now)
                {
                    administrador.SetPassword(model.NewPassword); 
                    administrador.PasswordResetToken = null; 
                    administrador.ResetTokenExpiration = null;
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Contraseña actualizada correctamente." });
                }

                return Json(new { success = false, message = "Token no válido o expirado." });
            }

            return View("ResetPassword", model);
        }

        private async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient("smtp.tu-servidor.com"))
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("no-reply@gmail.com"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}*/