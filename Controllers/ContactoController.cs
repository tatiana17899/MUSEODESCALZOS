using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using MuseoDescalzos.Models;
using MUSEODESCALZOS.Data;
using ClasificacionModelo;
using MUSEODESCALZOS.Services;
using System.ComponentModel.DataAnnotations;

namespace MUSEO_DE_LOS_DESCALZOS.Controllers
{

    public class ContactoController : Controller
    {
        private readonly ILogger<ContactoController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;


        public ContactoController(ILogger<ContactoController> logger,ApplicationDbContext context, 
        EmailService emailService)
        {
            _logger = logger;
            _context = context;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var miscontactos = from o in _context.DataContacto select o;
            _logger.LogDebug("contactos {miscontactos}", miscontactos);
            var viewModel = new ContactoViewModel{
                FormContacto = new Contacto(),
                ListContacto = miscontactos
            };
             _logger.LogDebug("viewModel {viewModel}", viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Enviar(ContactoViewModel viewModel)
        {
            _logger.LogDebug("Ingreso a Enviar Mensaje");

            
            var contacto = new Contacto
            {
                Name = viewModel.FormContacto.Name,
                Email = viewModel.FormContacto.Email,
                Message = viewModel.FormContacto.Message
            };

            
            MLModelTextClasification.ModelInput sampleData = new MLModelTextClasification.ModelInput()
            {
                Comment = contacto.Message
            };

            MLModelTextClasification.ModelOutput output = MLModelTextClasification.Predict(sampleData);

            //Console.WriteLine($"{output.Label}{output.PredictedLabel}");

           //output.Score.ToList().ForEach(score => Console.WriteLine(score));

            var sortedScoresWithLabel = MLModelTextClasification.PredictAllLabels(sampleData);
            var scoreKeyFirst = sortedScoresWithLabel.ToList()[0].Key;
            var scoreValueFirst = sortedScoresWithLabel.ToList()[0].Value;
            var scoreKeySecond = sortedScoresWithLabel.ToList()[1].Key;
            var scoreValueSecond = sortedScoresWithLabel.ToList()[1].Value;

           if (scoreKeyFirst == "1")
            {
                if(scoreValueFirst > 0.5)
                {
                    contacto.Categoria = "Positivo";
                }
                else
                {
                    contacto.Categoria = "Negativo";
                }
            }else{
                if(scoreValueFirst > 0.5)
                {
                    contacto.Categoria = "Positivo";
                }
                else
                {
                    contacto.Categoria = "Negativo";
                }
            }


            Console.WriteLine($"{scoreKeyFirst,-40}{scoreValueFirst,-20}");
            Console.WriteLine($"{scoreKeySecond,-40}{scoreValueSecond,-20}");

            /*foreach (var score in sortedScoresWithLabel)
            {
                Console.WriteLine($"{score.Key,-40}{score.Value,-20}");
            }*/


            _context.Add(contacto);
            _context.SaveChanges();

            var subject = "Nuevo Comentario del Cliente";
            var body = @"
                <div style='font-family: Arial, sans-serif; border: 1px solid #ddd; padding: 20px; max-width: 600px; margin: auto;'>
                    <h2 style='text-align: center; color: #2c3e50;'>MUSEO DE LOS DESCALZOS</h2>
                    <h3 style='text-align: center; color: #16a085;'>¡FELICITACIONES SE REGISTRÓ TU COMENTARIO!</h3>
                    <div style='text-align: center; margin: 20px 0;'>
                        <img src='https://i.imgur.com/55ARXd6.jpg' alt='Imagen Museo' style='max-width: 100%; height: auto;'/>
                    </div>
                </div>
            ";


            if (string.IsNullOrEmpty(viewModel.FormContacto.Email) || !new EmailAddressAttribute().IsValid(viewModel.FormContacto.Email))
            {
                TempData["ErrorMessage"] = "Correo electrónico del cliente no válido.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _emailService.SendEmail(subject, body, viewModel.FormContacto.Email);
                TempData["SuccessMessage"] = "Mensaje enviado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Hubo un problema al enviar el mensaje: " + ex.Message + "\n" + ex.StackTrace;
                _logger.LogError(ex, "Error al enviar el correo");
            }

            ViewData["Message"] = "Se registro el contacto"; 
            
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}