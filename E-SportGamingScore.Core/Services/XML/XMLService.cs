using E_SportGamingScore.Core.Constants;
using E_SportGamingScore.Core.Contracts.XML;
using E_SportGamingScore.Infrastructure.Data;
using E_SportGamingScore.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace E_SportGamingScore.Core.Services.XML
{
    public class XMLService : BackgroundService, IXmlService
    {
        private readonly ApplicationDbContext data;
        private readonly IServiceProvider serviceProvider;

        public XMLService(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            this.data = data;
            this.serviceProvider = serviceProvider;
        }

        public async Task ParseAndStoreData(IServiceProvider serviceProvider)
        {
            var xmlFilePath = XmlConstants.xmlFilePath;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XmlDocument));
                using (StreamReader reader = new StreamReader(xmlFilePath, detectEncodingFromByteOrderMarks: true))
                {
                    XmlDocument xmlDoc = (XmlDocument)serializer.Deserialize(reader);
                    XmlNodeList sportNodes = xmlDoc.GetElementsByTagName("Sport");

                    foreach (XmlNode sportNode in sportNodes)
                    {
                        var sportIdString = sportNode.Attributes["ID"].Value;
                        var sportId = int.Parse(sportIdString);

                        using (var scope = serviceProvider.CreateScope())
                        {
                            var data = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                            var existingSport = data.Sports.FirstOrDefault(s => s.SportId == sportId);

                            if (existingSport == null)
                            {
                                Sport sport = new Sport
                                {
                                    SportId = sportId,
                                    SportName = sportNode.Attributes["Name"].Value
                                };

                                data.Sports.Add(sport);
                            }
                            else
                            {
                                existingSport.SportName = sportNode.Attributes["Name"].Value;
                            }

                            XmlNodeList eventNodes = sportNode.SelectNodes("Event");
                            foreach (XmlNode eventNode in eventNodes)
                            {
                                var eventIdString = eventNode.Attributes["ID"].Value;
                                var eventId = int.Parse(eventIdString);

                                var existingEvent = data.Events.FirstOrDefault(e => e.EventId == eventId);

                                if (existingEvent == null)
                                {
                                    Event events = new Event
                                    {
                                        EventId = eventId,
                                        EventName = eventNode.Attributes["Name"].Value,
                                        SportId = sportId
                                    };

                                    data.Events.Add(events);
                                }
                                else
                                {
                                    existingEvent.EventName = eventNode.Attributes["Name"].Value;
                                    existingEvent.SportId = sportId;
                                }

                                XmlNodeList matchNodes = eventNode.SelectNodes("Match");
                                foreach (XmlNode matchNode in matchNodes)
                                {
                                    var matchIdString = matchNode.Attributes["ID"].Value;
                                    var matchId = int.Parse(matchIdString);

                                    var existingMatch = data.Matches.FirstOrDefault(m => m.MatchId == matchId);

                                    if (existingMatch == null)
                                    {
                                        Match match = new Match
                                        {
                                            MatchId = matchId,
                                            MatchName = matchNode.Attributes["Name"].Value,
                                            MatchType = matchNode.Attributes["MatchType"].Value,
                                            StartDate = DateTime.Parse(matchNode.Attributes["StartDate"].Value),
                                            EventId = eventId
                                        };

                                        data.Matches.Add(match);
                                    }
                                    else
                                    {
                                        existingMatch.MatchName = matchNode.Attributes["Name"].Value;
                                        existingMatch.StartDate = DateTime.Parse(matchNode.Attributes["StartDate"].Value);
                                        existingMatch.EventId = eventId;
                                    }

                                    XmlNodeList betNodes = matchNode.SelectNodes("Bet");
                                    foreach (XmlNode betNode in betNodes)
                                    {
                                        var betIdString = betNode.Attributes["ID"].Value;
                                        var betId = int.Parse(betIdString);
                                        var betName = betNode.Attributes["Name"].Value;
                                        var betIsLive = bool.Parse(betNode.Attributes["IsLive"].Value);

                                        var existingBet = data.Bets.FirstOrDefault(m => m.BetId == betId);

                                        if (existingBet == null)
                                        {
                                            Bet bet = new Bet
                                            {
                                                BetId = betId,
                                                BetName = betName,
                                                MatchId = matchId,
                                            };

                                            data.Bets.Add(bet);
                                        }
                                        else
                                        {
                                            existingBet.BetName = betName;
                                            existingBet.MatchId = matchId;
                                            existingBet.IsLive = betIsLive;
                                        }

                                        XmlNodeList oddsNodes = betNode.SelectNodes("Odd");
                                        foreach (XmlNode oddNode in oddsNodes)
                                        {
                                            var oddIdStrin = oddNode.Attributes["ID"]?.Value;
                                            var oddId = int.Parse(oddIdStrin);
                                            var oddValueString = oddNode.Attributes["Value"]?.Value;
                                            var oddValue = decimal.Parse(oddValueString, CultureInfo.InvariantCulture);
                                            var specialBetValueString = oddNode.Attributes["SpecialBetValue"]?.Value;
                                            decimal? specialBetValue = !string.IsNullOrEmpty(specialBetValueString)
                                            ? decimal.Parse(specialBetValueString, CultureInfo.InvariantCulture)
                                            : (decimal?)null;

                                            var existingOdd = data.Odds.FirstOrDefault(m => m.OddId == oddId);

                                            if (existingOdd == null)
                                            {
                                                Odd odd = new Odd
                                                {
                                                    OddId = oddId,
                                                    Name = oddNode.Attributes["Name"].Value,
                                                    OddValue = oddValue,
                                                    SpecialBetValue = specialBetValue,
                                                    BetId = betId,
                                                   
                                                };

                                                data.Odds.Add(odd);
                                            }
                                            else
                                            {
                                                existingOdd.Name = oddNode.Attributes["Name"].Value;
                                                existingOdd.OddValue = oddValue;
                                                existingOdd.SpecialBetValue = specialBetValue;
                                                existingOdd.BetId = betId;
                                            }
                                        }
                                    }
                                }
                            }

                             data.SaveChanges();
                        }
                    }
                }
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
               await ParseAndStoreData(serviceProvider);
                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
