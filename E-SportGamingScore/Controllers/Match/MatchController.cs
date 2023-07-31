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

        public async Task<IActionResult> AllMatchesFor24H() => this.View(await matchesService.AllMatchesFor24H());
        
        public async Task<IActionResult> AllMatches() => this.View(await matchesService.AllMatches());
       
        public async Task<IActionResult> GetById(int matchId) => this.View(await matchesService.GetMatchById(matchId));
        
    }
}
