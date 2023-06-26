using E_SportGamingScore.Core.Contracts.BackGround;
using E_SportGamingScore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_SportGamingScore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBackgroundTaskService backgroundTaskService;
        public HomeController(ILogger<HomeController> logger, IBackgroundTaskService backgroundTaskService)
        {
            _logger = logger;
            this.backgroundTaskService = backgroundTaskService;
        }

        public async Task<IActionResult> Index(CancellationToken stoppingToken)
        {
            await backgroundTaskService.StartBackgroundTask(stoppingToken);
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