using E_SportGamingScore.Core.Contracts.Matches;
using E_SportGamingScore.Core.Contracts.Sports;
using E_SportGamingScore.Core.ViewModels.Bets;
using E_SportGamingScore.Core.ViewModels.Matches;
using E_SportGamingScore.Core.ViewModels.Ods;
using E_SportGamingScore.Infrastructure.Data;
using E_SportGamingScore.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography;

namespace E_SportGamingScore.Core.Services.Matches
{
    public class MatchesService : BackgroundService, IMatchService
    {
        private readonly ApplicationDbContext data;
        private readonly ISportService sportService;


        public MatchesService(ApplicationDbContext data, ISportService sportService)
        {
            this.data = data;
            this.sportService = sportService;
        }

        public GetMatchById GetMatchById(int matchId)
        {
            var match = this.data.Matches
                 .Where(m => m.MatchId == matchId)
                 .Select(m => new GetMatchById
                 {
                     MatchId = m.MatchId,
                     MatchName = m.MatchName,
                     MatchType = m.MatchType,
                     StartDate = m.StartDate,
                     Bets = m.Bets.Select(bet => new BetViewModel
                     {
                         BetId = bet.BetId,
                         BetName = bet.BetName,
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
                 }).FirstOrDefault();

            return match;
        }

        public IEnumerable<AllMatchesFor24H> AllMatchesFor24H()
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime next24Hours = currentDateTime.AddHours(24);

            var matches = data.Matches
             .Include(match => match.Bets)
             .ThenInclude(bet => bet.Odds)
             .Where(match => match.StartDate >= currentDateTime && match.StartDate <= next24Hours)
             .ToList();

            var bets = matches.SelectMany(match => match.Bets).ToList();

            var oddsWithSpecialBetValue = bets
                .SelectMany(bet => bet.Odds)
                .Where(odd => odd.SpecialBetValue != null)
                .ToList();

            var matchesFor24Hs = matches
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


            return matchesFor24Hs;
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
        });

            return allMatches;
        }

        public async Task CheckForChanges()
        {

            try
            {
                var currentSports = sportService.AllSports().ToList();

                foreach (var currentSport in currentSports)
                {
                    var existingSport = data.Sports.FirstOrDefault(s => s.SportId == currentSport.SportId);

                    if (existingSport == null)
                    {
                        Sport sport = new Sport
                        {
                            SportId = currentSport.SportId,
                            SportName = currentSport.SportName
                        };

                        data.Sports.Add(sport);
                    }
                    else
                    {
                        existingSport.SportName = currentSport.SportName;
                    }

                    foreach (var currentEvent in currentSport.Events)
                    {
                        var existingEvent = data.Events.FirstOrDefault(e => e.EventId == currentEvent.EventId);

                        if (existingEvent == null)
                        {
                            Event events = new Event
                            {
                                EventId = currentEvent.EventId,
                                EventName = currentEvent.EventName,
                                IsLive = currentEvent.IsLive,
                                CategoryId = currentEvent.CategoryId,
                                SportId = currentSport.SportId
                            };

                            data.Events.Add(events);
                        }
                        else
                        {
                            existingEvent.EventName = currentEvent.EventName;
                            existingEvent.IsLive = currentEvent.IsLive;
                            existingEvent.CategoryId = currentEvent.CategoryId;
                            existingEvent.SportId = currentSport.SportId;
                        }

                        foreach (var currentMatch in currentEvent.Matches)
                        {
                            var existingMatch = data.Matches.FirstOrDefault(m => m.MatchId == currentMatch.MatchId);

                            if (existingMatch == null)
                            {
                                Match match = new Match
                                {
                                    MatchId = currentMatch.MatchId,
                                    MatchName = currentMatch.MatchName,
                                    MatchType = currentMatch.MatchType,
                                    StartDate = currentMatch.MatchStartDate,
                                    EventId = currentEvent.EventId
                                };

                                data.Matches.Add(match);
                            }
                            else
                            {
                                existingMatch.MatchName = currentMatch.MatchName;
                                existingMatch.MatchType = currentMatch.MatchType;
                                existingMatch.StartDate = currentMatch.MatchStartDate;
                                existingMatch.EventId = currentEvent.EventId;
                            }

                            foreach (var currentBet in currentMatch.Bets)
                            {
                                var existingBet = data.Bets.FirstOrDefault(b => b.BetId == currentBet.BetId);

                                if (existingBet == null)
                                {
                                    Bet bet = new Bet
                                    {
                                        BetId = currentBet.BetId,
                                        BetName = currentBet.BetName,
                                        MatchId = currentMatch.MatchId,
                                        IsLive = currentBet.IsBetLive
                                    };

                                    data.Bets.Add(bet);
                                }
                                else
                                {
                                    existingBet.BetName = currentBet.BetName;
                                    existingBet.MatchId = currentMatch.MatchId;
                                    existingBet.IsLive = currentBet.IsBetLive;
                                }

                                foreach (var currentOdd in currentBet.Odds)
                                {
                                    var existingOdd = data.Odds.FirstOrDefault(o => o.OddId == currentOdd.OddId);

                                    if (existingOdd == null)
                                    {
                                        Odd odd = new Odd
                                        {
                                            OddId = currentOdd.OddId,
                                            Name = currentOdd.OddName,
                                            OddValue = currentOdd.OddValue,
                                            SpecialBetValue = currentOdd.SpecialBetValue,
                                            BetId = currentBet.BetId
                                        };

                                        data.Odds.Add(odd);
                                    }
                                    else
                                    {
                                        existingOdd.Name = currentOdd.OddName;
                                        existingOdd.OddValue = currentOdd.OddValue;
                                        existingOdd.SpecialBetValue = currentOdd.SpecialBetValue;
                                        existingOdd.BetId = currentBet.BetId;
                                    }
                                }
                            }
                        }
                    }
                }
                await data.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Time(CancellationToken stoppingToken)
        {
            await this.ExecuteAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await this.CheckForChanges();
                await Task.Delay(1000, stoppingToken);
            }
        }

    }
}
