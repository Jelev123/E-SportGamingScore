using E_SportGamingScore.Core.Contracts.BackGround;
using E_SportGamingScore.Core.Contracts.Backround;
using E_SportGamingScore.Core.Services.Matches;
using E_SportGamingScore.Core.Services.XML;
using E_SportGamingScore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_SportGamingScore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBackgroundTaskXmlService backgroundTaskService;
        private readonly IBackgroundTaskMatchService backgroundCheckMatchesService;
        public HomeController(ILogger<HomeController> logger, IBackgroundTaskXmlService backgroundTaskService, IBackgroundTaskMatchService backgroundCheckMatchesService)
        {
            _logger = logger;
            this.backgroundTaskService = backgroundTaskService;
            this.backgroundCheckMatchesService = backgroundCheckMatchesService;
        }

        public async Task<IActionResult> Index(CancellationToken stoppingToken)
        {
            var task1 = backgroundCheckMatchesService.StartBackgroundTask(stoppingToken);
            var task2 = backgroundTaskService.StartBackgroundTask(stoppingToken);

            await Task.WhenAll(task1, task2);

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