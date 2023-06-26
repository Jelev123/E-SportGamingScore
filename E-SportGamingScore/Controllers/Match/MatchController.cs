using E_SportGamingScore.Core.Contracts.Matches;
using Microsoft.AspNetCore.Mvc;

namespace E_SportGamingScore.Controllers.Match
{
    public class MatchController : Controller
    {
        private readonly IMatchService matchesService;

        public MatchController(IMatchService matchesService)
        {
            this.matchesService = matchesService;
        }

        public IActionResult AllMatchesFor24H() => this.View(matchesService.AllMatchesFor24H());
        
        public IActionResult AllMatches() => this.View(matchesService.AllMatches());
       
        public IActionResult GetById(int matchId) => this.View(matchesService.GetMatchById(matchId));
        
    }
}
