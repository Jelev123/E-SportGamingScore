using E_SportGamingScore.Core.Contracts.XML;
using E_SportGamingScore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_SportGamingScore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IXmlService xmlService;


        public HomeController(ILogger<HomeController> logger, IXmlService xmlService)
        {
            _logger = logger;
            this.xmlService = xmlService;
        }

        public IActionResult Index(CancellationToken stoppingToken)
        {
            xmlService.Time(stoppingToken);
            this.ViewData["LoggedMessage"] = "Message logged on the index page.";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}