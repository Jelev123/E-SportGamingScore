using E_SportGamingScore.Core.Contracts.BackGround;
using E_SportGamingScore.Core.Contracts.Backround;
using E_SportGamingScore.Core.Services.Background;
using E_SportGamingScore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_SportGamingScore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBackgroundTaskXmlService backgroundTaskXmlService;
        private readonly IBackgroundTaskMatchService backgroundCheckMatchesService;
        private readonly IBackgroundTaskBetService backgroundTaskBetService;
        private readonly IBackgroundTaskOddService backgroundTaskOddService;
        public HomeController(ILogger<HomeController> logger, IBackgroundTaskXmlService backgroundTaskXmlService, IBackgroundTaskMatchService backgroundCheckMatchesService, IBackgroundTaskBetService backgroundTaskBetService, IBackgroundTaskOddService backgroundTaskOddService)
        {
            _logger = logger;
            this.backgroundTaskXmlService = backgroundTaskXmlService;
            this.backgroundCheckMatchesService = backgroundCheckMatchesService;
            this.backgroundTaskBetService = backgroundTaskBetService;
            this.backgroundTaskOddService = backgroundTaskOddService;
        }

        public async Task<IActionResult> Index(CancellationToken stoppingToken)
        {
            var task1 = backgroundTaskXmlService.StartBackgroundTask(stoppingToken);
            var task2 = backgroundCheckMatchesService.StartBackgroundTask(stoppingToken);
            var task3 = backgroundTaskBetService.StartBackgroundTask(stoppingToken);
            var task4 = backgroundTaskOddService.StartBackgroundTask(stoppingToken);

            await Task.WhenAll(task1, task2, task3,task4);

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