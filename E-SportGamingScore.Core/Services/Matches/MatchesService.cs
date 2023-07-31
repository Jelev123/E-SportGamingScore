using E_SportGamingScore.Core.Contracts.Matches;
using E_SportGamingScore.Core.Contracts.Sports;
using E_SportGamingScore.Core.Enums;
using E_SportGamingScore.Core.ViewModels.Bets;
using E_SportGamingScore.Core.ViewModels.Matches;
using E_SportGamingScore.Core.ViewModels.Ods;
using E_SportGamingScore.Infrastructure.Data;
using E_SportGamingScore.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace E_SportGamingScore.Core.Services.Matches
{
    public class MatchesService : IMatchService
    {
        private readonly ApplicationDbContext data;
        private readonly ISportService sportService;
        private readonly IServiceScopeFactory serviceScopeFactory;



        public MatchesService(ApplicationDbContext data, ISportService sportService, IServiceScopeFactory serviceScopeFactory)
        {
            this.data = data;
            this.sportService = sportService;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<GetMatchById> GetMatchById(int matchId)
        {
            return await this.data.Matches
               .Where(m => m.MatchId == matchId)
               .Select(m => new GetMatchById
               {
                   MatchId = m.MatchId,
                   MatchName = m.MatchName,
                   MatchType = m.MatchType,
                   StartDate = m.StartDate,
                   ActiveMarkets = m.Bets
                       .Where(bet => bet.IsLive)
                       .Select(bet => new BetViewModel
                       {
                           BetId = bet.BetId,
                           BetName = bet.BetName,
                           IsBetLive = bet.IsLive,
                           Odds = bet.Odds.Select(odd => new OdsViewModel
                           {
                               OddId = odd.OddId,
                               OddName = odd.Name,
                               OddValue = odd.OddValue,
                               BetName = bet.BetName,
                               BetId = bet.BetId,
                               SpecialBetValue = odd.SpecialBetValue
                           }).ToList()
                       }).ToList(),
                   InactiveMarkets = m.Bets
                       .Where(bet => !bet.IsLive)
                       .Select(bet => new BetViewModel
                       {
                           BetId = bet.BetId,
                           BetName = bet.BetName,
                           IsBetLive = bet.IsLive,
                           Odds = bet.Odds.Select(odd => new OdsViewModel
                           {
                               OddId = odd.OddId,
                               OddName = odd.Name,
                               OddValue = odd.OddValue,
                               BetName = bet.BetName,
                               BetId = bet.BetId,
                               SpecialBetValue = odd.SpecialBetValue
                           }).ToList()
                       }).ToList(),
               }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AllMatchesFor24H>> AllMatchesFor24H()
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime next24Hours = currentDateTime.AddHours(24);

            var matches = await data.Matches
             .Include(match => match.Bets)
             .ThenInclude(bet => bet.Odds)
             .Where(match => match.StartDate >= currentDateTime && match.StartDate <= next24Hours)
             .ToListAsync();

            var bets = matches.SelectMany(match => match.Bets).ToList();

            var oddsWithSpecialBetValue = bets
                .SelectMany(bet => bet.Odds)
                .Where(odd => odd.SpecialBetValue != null)
                .ToList();

            return matches
                .Select(match => new AllMatchesFor24H
                {
                    MatchId = match.MatchId,
                    MatchStartDate = match.StartDate,
                    MatchType = match.MatchType,
                    MatchName = match.MatchName,
                    Bets = match.Bets
                        .Select(bet => new BetViewModel
                        {
                            MatchId = bet.MatchId,
                            BetId = bet.BetId,
                            BetName = bet.BetName,
                            IsBetLive = bet.IsLive,
                            Odds = oddsWithSpecialBetValue
                                .Where(odd => odd.BetId == bet.BetId)
                                .Take(2)
                                .Concat(bet.Odds
                                    .Where(odd => odd.SpecialBetValue == null))
                                .Select(odd => new OdsViewModel
                                {
                                    OddName = odd.Name,
                                    OddValue = odd.OddValue,
                                    SpecialBetValue = odd.SpecialBetValue,
                                    BetId = odd.BetId,
                                    BetName = bet.BetName
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();
        }
        public async Task<IEnumerable<AllMatchesFor24H>> AllMatches()
        {
            return await this.data.Matches
               .Select(match => new AllMatchesFor24H
               {
                   MatchId = match.MatchId,
                   MatchName = match.MatchName,
                   MatchType = match.MatchType,
                   MatchStartDate = match.StartDate,
                   EventId = match.EventId,
                   Bets = match.Bets.Select(s => new BetViewModel
                   {
                       BetId = s.BetId,
                       BetName = s.BetName,
                       Odds = (List<OdsViewModel>)match.Bets
                       .SelectMany(bet => bet.Odds.Select(odd => new OdsViewModel
                       {
                           OddId = odd.OddId,
                           OddName = odd.Name,
                           OddValue = odd.OddValue,
                           BetName = bet.BetName,
                           BetId = bet.BetId,
                           SpecialBetValue = odd.SpecialBetValue
                       }))
                   }).ToList(),
               })
               .ToListAsync();
        }


        public async Task<IEnumerable<UpdateMatchesMessagesViewModel>> GenerateMatchUpdateMessages()
        {
            var updatedMatch = new List<UpdateMatchMessages>();
            var testUpdateMatchMessages = new List<UpdateMatchesMessagesViewModel>();
            var currentSports = await this.sportService.AllSports();


            //Тhe idea of these lists is that the method will accept information in the form of an information lists аnd this method will process it and send the information to the message table

            testUpdateMatchMessages.Add(new UpdateMatchesMessagesViewModel
            {
                MatchId = 2996654,
                MatchName = "match 1",
                MatchType = "Test Type",
                MatchStartDate = DateTime.Now,
                ActionType = MessageActionTypeEnum.Update.ToString(),
                TimeStamp = DateTime.Now
            }); testUpdateMatchMessages.Add(new UpdateMatchesMessagesViewModel
            {
                MatchId = 2996654,
                MatchName = "match 2",
                MatchType = "Test Type2",
                MatchStartDate = DateTime.Now,
                ActionType = MessageActionTypeEnum.Update.ToString(),
                TimeStamp = DateTime.Now
            });

            var uniqueUpdateMatchMessages = testUpdateMatchMessages
             .GroupBy(u => new { u.MatchId })
             .Select(g => g.First())
             .ToList();

            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    foreach (var currentSport in currentSports)
                    {
                        foreach (var currentEvent in currentSport.Events)
                        {
                            foreach (var currentMatch in currentEvent.Matches)
                            {
                                var matchingUpdatematch = uniqueUpdateMatchMessages.FirstOrDefault(
                                           u => u.MatchId == currentMatch.MatchId);

                                if (matchingUpdatematch != null)
                                {
                                    currentMatch.MatchName = matchingUpdatematch.MatchName;
                                    currentMatch.MatchType = matchingUpdatematch.MatchType;
                                    currentMatch.MatchStartDate = matchingUpdatematch.MatchStartDate;

                                    var update = new UpdateMatchMessages
                                    {
                                        MatchId = currentMatch.MatchId,
                                        MatchName = currentMatch.MatchName,
                                        MatchType = currentMatch.MatchType,
                                        MatchStartDate = currentMatch.MatchStartDate,
                                        ActionType = MessageActionTypeEnum.Update.ToString(),
                                        TimeStamp = DateTime.Now
                                    };

                                    updatedMatch.Add(update);
                                    context.UpdateMatchMessages.Add(update);
                                }
                            }
                        }
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return (IEnumerable<UpdateMatchesMessagesViewModel>)updatedMatch;
        }
    }
}

