using E_SportGamingScore.Core.Contracts.Sports;
using E_SportGamingScore.Core.ViewModels.Bets;
using E_SportGamingScore.Core.ViewModels.Events;
using E_SportGamingScore.Core.ViewModels.Matches;
using E_SportGamingScore.Core.ViewModels.Ods;
using E_SportGamingScore.Core.ViewModels.Sports;
using E_SportGamingScore.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace E_SportGamingScore.Core.Services.Sports
{
    public class SportService : ISportService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;


        public SportService(ApplicationDbContext data, IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public IEnumerable<SportsViewModel> AllSports()
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                return context.Sports.
               Select(s => new SportsViewModel
               {
                   SportId = s.SportId,
                   SportName = s.SportName,
                   Events = (List<EventViewModel>)s.Events.Select(s => new EventViewModel
                   {
                       EventId = s.EventId,
                       EventName = s.EventName,
                       Matches = (List<AllMatchesFor24H>)s.Matches.Select(s => new AllMatchesFor24H
                       {
                           MatchId = s.MatchId,
                           MatchName = s.MatchName,
                           MatchType = s.MatchType,
                           MatchStartDate = s.StartDate,
                           Bets = (List<BetViewModel>)s.Bets.Select(s => new BetViewModel
                           {
                               BetId = s.BetId,
                               BetName = s.BetName,
                               Odds = s.Odds.Select(s => new OdsViewModel
                               {
                                   OddId = s.OddId,
                                   OddName = s.Name,
                                   OddValue = s.OddValue,
                                   SpecialBetValue = s.SpecialBetValue,
                               }).ToList()

                           })
                       })
                   })
               }).ToList();
            }
        }
    }
}
