using E_SportGamingScore.Core.Contracts.Matches;
using E_SportGamingScore.Core.ViewModels.Matches;
using E_SportGamingScore.Core.ViewModels.Ods;
using E_SportGamingScore.Infrastructure.Data;
using E_SportGamingScore.Infrastructure.Models;
using System.Linq;

namespace E_SportGamingScore.Core.Services.Matches
{
    public class MatchesService : IMatches
    {
        private readonly ApplicationDbContext data;

        public MatchesService(ApplicationDbContext data)
        {
            this.data = data;
        }



        public GetMatchById GetMatchById(int matchId)
        {
            var match = this.data.Matches.Where(m => m.MatchId == matchId)
                .Select(m => new GetMatchById
                {
                    MatchId = matchId,
                    MatchName = m.MatchName,
                    StartDate = m.StartDate,
                    Ods = m.Bets.SelectMany(s => s.Odds).Select(s => new OdsViewModel
                    {
                        BetName = s.Bet.BetName,
                        OddName = s.Name,
                        OddValue = s.OddValue,
                        SpecialBetValue = s.SpecialBetValue
                    }).ToList(),

                }).FirstOrDefault();

            return match;
        }

        public IEnumerable<AllMatchesFor24H> AllMatchesFor24H()
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime next24Hours = currentDateTime.AddHours(24);

            var filteredMatches = data.Matches
                 .Where(match => match.StartDate >= currentDateTime && match.StartDate <= next24Hours);

         
            var matchesWithSpecialBetValue = filteredMatches
                 .Where(m => m.Bets.Any(b => b.Odds.Any(o => o.SpecialBetValue != null)))
                 .Select(s => s.Bets.SelectMany(b => b.Odds)
                     .Where(o => o.SpecialBetValue != null)
                     .Take(2)
                     .Select(o => new AllMatchesFor24H
                     {
                         MatchName = s.MatchName,
                         MatchType = s.MatchType,
                         MatchId = s.MatchId,
                         MatchStartDate = s.StartDate,
                         IsBerAlive = s.IsLive,
                         Odds = new List<OdsViewModel>
                         {
                            new OdsViewModel
                            {
                                BetName = o.Bet.BetName,
                                OddName = o.Name,
                                OddValue = o.OddValue,
                                SpecialBetValue = o.SpecialBetValue,
                                BetId = o.BetId
                            }
                         }
                     }))
                 .ToList();

            var matchesWithoutSpecialBetValue = filteredMatches
                .Where(m => m.Bets.Any(b => b.Odds.Any(o => o.SpecialBetValue == null)))
                .Select(s => s.Bets.SelectMany(b => b.Odds)
                    .Where(o => o.SpecialBetValue == null)
                    .Select(o => new AllMatchesFor24H
                    {
                        MatchName = s.MatchName,
                        MatchType = s.MatchType,
                        MatchId = s.MatchId,
                        MatchStartDate = s.StartDate,
                        IsBerAlive= s.IsLive,
                        Odds = new List<OdsViewModel>
                        {
                new OdsViewModel
                {
                    BetName = o.Bet.BetName,
                    OddName = o.Name,
                    OddValue = o.OddValue,
                    SpecialBetValue = o.SpecialBetValue,
                    BetId = o.BetId
                }
                        }
                    }))
                .ToList();

            var combinedMatches = matchesWithSpecialBetValue.Concat(matchesWithoutSpecialBetValue)
                .SelectMany(x => x)
                .ToList();

            return combinedMatches;

        }
        public IEnumerable<AllMatchesFor24H> AllMatches()
        {
            var allMatches = this.data.Matches
        .Select(match => new AllMatchesFor24H
        {
            MatchId = match.MatchId,
            MatchName = match.MatchName,
            MatchType = match.MatchType,
            MatchStartDate = match.StartDate,
            Odds = (List<OdsViewModel>)match.Bets
                .SelectMany(bet => bet.Odds.Select(odd => new OdsViewModel
                {
                    OddId = odd.OddId,
                    OddName = odd.Name,
                    OddValue = odd.OddValue,
                    BetName = bet.BetName,
                    SpecialBetValue = odd.SpecialBetValue
                }))
        });

            return allMatches;
        }

        public void CheckFroChanges()
        {
            var allMatches = this.AllMatches();
            var ods = new List<decimal>();

            foreach (var match in allMatches)
            {
                var odds = match.Odds;
                foreach (var item in odds)
                {
                    ods.Add(item.OddValue);
                    ods.Add((item.SpecialBetValue != null) ? (decimal)item.SpecialBetValue : decimal.MinValue);
                }
            }
            data.SaveChanges();

        }
    }
}
