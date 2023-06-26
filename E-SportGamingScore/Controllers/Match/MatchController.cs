using E_SportGamingScore.Core.Contracts.Matches;
using E_SportGamingScore.Core.ViewModels.Ods;
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

        public IActionResult AllMatchesFor24H()
        {
            return this.View(matchesService.AllMatchesFor24H());
        } 
        public IActionResult AllMatches()
        {
            return View(matchesService.AllMatches());
        }

        //public IActionResult CheckForChanges()
        //{
        //    this.matchesService.CheckFroChanges();
        //    return Ok();
        //}

        public IActionResult GetById(int matchId) => this.View(matchesService.GetMatchById(matchId));
        
    }
}
