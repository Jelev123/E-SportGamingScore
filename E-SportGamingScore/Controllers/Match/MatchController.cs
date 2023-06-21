using E_SportGamingScore.Core.Contracts.Matches;
using Microsoft.AspNetCore.Mvc;

namespace E_SportGamingScore.Controllers.Match
{
    public class MatchController : Controller
    {
        private readonly IMatches matchesService;

        public MatchController(IMatches matchesService)
        {
            this.matchesService = matchesService;
        }

        public IActionResult AllMatchesFor24H()
        {
            return View(matchesService.AllMatches());
        }

        public IActionResult GetById(int matchId) => View(matchesService.GetMatchById(matchId));
        
    }
}
