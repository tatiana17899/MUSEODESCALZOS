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

namespace MUSEO_DE_LOS_DESCALZOS.Controllers
{

    public class ContactoController : Controller
    {
        private readonly ILogger<ContactoController> _logger;
        private readonly ApplicationDbContext _context;

        public ContactoController(ILogger<ContactoController> logger,ApplicationDbContext context)
        {
            _logger = logger;
             _context = context;
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

            Console.WriteLine($"{scoreKeyFirst,-40}{scoreValueFirst,-20}");
            Console.WriteLine($"{scoreKeySecond,-40}{scoreValueSecond,-20}");

            /*foreach (var score in sortedScoresWithLabel)
            {
                Console.WriteLine($"{score.Key,-40}{score.Value,-20}");
            }*/


            _context.Add(contacto);
            _context.SaveChanges();

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