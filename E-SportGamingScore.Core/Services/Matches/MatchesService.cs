using E_SportGamingScore.Core.Contracts.Matches;
using E_SportGamingScore.Core.ViewModels.Matches;
using E_SportGamingScore.Core.ViewModels.Ods;
using E_SportGamingScore.Infrastructure.Data;

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
                    MatchName = m.MatchName,
                    BetName = m.Bets.Select(s => new OdsViewModel
                    {
                        BetName = s.BetName
                    }).ToList(),
                    StartDate = m.StartDate,
                    Ods = m.Bets.SelectMany(s => s.Odds).Select(s => new OdsViewModel
                    {
                        OddName = s.Name,
                        OddValue = s.OddValue,
                        SpecialBetValue = s.SpecialBetValue
                    }).ToList(),

                }).FirstOrDefault();

            return match;
        }

        IEnumerable<AllMatchesFor24H> IMatches.AllMatches()
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime next24Hours = currentDateTime.AddHours(24);

            var matchesWithSpecBetValue = data.Matches
            .Where(match => match.Bets.Any(o => o.Odds.Any(s => s.SpecialBetValue != null)))
            .Where(match => match.StartDate >= currentDateTime && match.StartDate <= next24Hours);

            var matches = data.Matches
            .Where(match => match.Bets.Any(o => o.Odds.Any(s => s.SpecialBetValue != null)) &&
                            match.StartDate >= currentDateTime && match.StartDate <= next24Hours)
            .SelectMany(m => m.Bets)
            .Where(b => b.Odds.Any(o => o.SpecialBetValue != null))
            .Select(b => new AllMatchesFor24H
            {
                MatchId = b.Match.MatchId,
                MatchName = b.Match.MatchName,
                MatchType = b.Match.MatchType,
                BetNames = b.BetName,
                Odds = b.Odds.Select(s => new OdsViewModel
                {
                    OddName = s.Name,
                    OddValue = s.OddValue,
                    SpecialBetValue = s.SpecialBetValue
                }).ToList()
            })
     .ToList();



            return matches;
        }
    }
}
