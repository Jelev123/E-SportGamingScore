using E_SportGamingScore.Core.Contracts.Matches;
using E_SportGamingScore.Core.Contracts.XML;
using E_SportGamingScore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Timers;

namespace E_SportGamingScore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IXmlService xmlService;
        private readonly IMatchService matchService;

        public HomeController(ILogger<HomeController> logger, IXmlService xmlService, IMatchService matchService)
        {
            _logger = logger;
            this.xmlService = xmlService;
            this.matchService = matchService;
        }

        public async Task<IActionResult> Index(CancellationToken stoppingToken)
        {
            //await xmlService.Time(stoppingToken);
           await matchService.Time(stoppingToken);
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