using E_SportGamingScore.Core.Contracts.Backround;
using E_SportGamingScore.Core.Services.Bets;
using E_SportGamingScore.Core.Services.Matches;
using E_SportGamingScore.Core.Services.Odd;
using E_SportGamingScore.Core.Services.XML;
using E_SportGamingScore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_SportGamingScore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEnumerable<IBackgroundTaskService> backgroundTaskServices;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, IEnumerable<IBackgroundTaskService> backgroundTaskServices)
        {
            _logger = logger;
            this.backgroundTaskServices = backgroundTaskServices;
        }

        public async Task<IActionResult> Index(CancellationToken stoppingToken)
        {
            var xml = backgroundTaskServices.FirstOrDefault(s => s.GetType() == typeof(BackgroundTaskXmlService));
            var match = backgroundTaskServices.FirstOrDefault(s => s.GetType() == typeof(BackgroundTaskMatchesService));
            var bet = backgroundTaskServices.FirstOrDefault(s => s.GetType() == typeof(BackgroundTaskBetService));
            var odd = backgroundTaskServices.FirstOrDefault(s => s.GetType() == typeof(BackgroundTaskOddService));

            var task1 = xml.StartBackgroundTask(stoppingToken);
            var task2 = match.StartBackgroundTask(stoppingToken);
            var task3 = bet.StartBackgroundTask(stoppingToken);
            var task4 = odd.StartBackgroundTask(stoppingToken);

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