using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.AI.Language.Conversations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MUSEODESCALZOS.Data;

namespace MUSEODESCALZOS.Controllers
{
    public class Chatbot : Controller
    {
        private readonly ConversationAnalysisClient _client;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Chatbot> _logger;

        public Chatbot(ILogger<Chatbot> logger, IConfiguration configuration, ApplicationDbContext context,
        ConversationAnalysisClient _client )
        {
             _configuration = configuration;
             _context = context;
            _logger = logger;
            _client = new ConversationAnalysisClient(
            new Uri(_configuration["Azure:Language:Endpoint"]),
            new AzureKeyCredential(_configuration["Azure:Language:ApiKey"]));
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}