using E_SportGamingScore.Core.Contracts.XML;
using E_SportGamingScore.Infrastructure.Data;
using E_SportGamingScore.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace E_SportGamingScore.Core.Services.XML
{
    public class XMLService : IXML
    {
        private readonly ApplicationDbContext data;
        
        public XMLService(ApplicationDbContext data)
        {
            this.data = data;
            
        }

        public void ParseAndStoreData()
        {
            //XDocument xmlDoc = XDocument.Parse(xmlData);

            var xmlFilePath = @"C:\Users\User\Desktop\sportsxml.xml";

            using (StreamReader reader = new StreamReader(xmlFilePath, detectEncodingFromByteOrderMarks: true))
            {
                XDocument xmlDoc = XDocument.Load(reader);

                foreach (XElement sportElement in xmlDoc.Descendants("Sport"))
                {
                    var sportName = sportElement.Attribute("Name")?.Value;
                    var sportIdString = sportElement.Attribute("ID")?.Value;
                    var sportId = int.Parse(sportIdString);
                    Sport sport = new Sport
                    {
                        SportId = sportId,
                        SportName = sportName,
                    };


                    foreach (XElement eventElement in sportElement.Elements("Event"))
                    {
                        var eventName = eventElement.Attribute("Name")?.Value;
                        var eventIdString = eventElement.Attribute("ID")?.Value;
                        var eventId = int.Parse(eventIdString);
                        var isLiveString = eventElement.Attribute("IsLive")?.Value;
                        var isLive = bool.Parse(isLiveString);
                        var categoryIdString = eventElement.Attribute("CategoryID")?.Value;
                        var categoryId = int.Parse(categoryIdString);

                        Event events = new Event
                        {
                            EventId = eventId,
                            CategoryId = categoryId,
                            EventName = eventName,
                            IsLive = isLive,
                            Sport = sport,
                        };

                        foreach (XElement matchElement in eventElement.Elements("Match"))
                        {
                            var matchName = matchElement.Attribute("Name")?.Value;
                            var matchIdString = matchElement.Attribute("ID")?.Value;
                            var matchId = int.Parse(matchIdString);
                            var matchStartDateString = matchElement.Attribute("StartDate")?.Value;
                            var matchDate = DateTime.Parse(matchStartDateString);
                            var matchType = matchElement.Attribute("MatchType")?.Value;

                            Match match = new Match
                            {
                                MatchId = matchId,
                                MatchName = matchName,
                                MatchType = matchType,
                                StartDate = matchDate,
                                EventId = eventId,
                            };


                            foreach (XElement betElement in matchElement.Elements("Bet"))
                            {
                                var betName = betElement.Attribute("Name")?.Value;
                                var betIdString = betElement.Attribute("ID")?.Value;
                                var betId = int.Parse(betIdString);
                                var isLiveBet = betElement.Attribute("IsLive")?.Value;
                                var betIsLive = bool.Parse(isLiveBet);

                                Bet bet = new Bet
                                {
                                    BetId = betId,
                                    BetName = betName,
                                    IsLive = betIsLive,
                                    MatchId = matchId,
                                };


                                foreach (XElement oddElement in betElement.Elements("Odd"))
                                {
                                    var oddName = oddElement.Attribute("Name")?.Value;
                                    var oddIdString = oddElement.Attribute("ID")?.Value;
                                    var oddId = int.Parse(oddIdString);
                                    var oddValueString = oddElement.Attribute("Value")?.Value;
                                    decimal oddValue = decimal.Parse(oddValueString, CultureInfo.InvariantCulture);
                                    var specialBetValueString = oddElement.Attribute("SpecialBetValue")?.Value;
                                    decimal? specialBetValue = !string.IsNullOrEmpty(specialBetValueString)
                                  ? decimal.Parse(specialBetValueString, CultureInfo.InvariantCulture)
                                  : (decimal?)null;

                                    Odd odd = new Odd
                                    {
                                        OddId = oddId,
                                        Name = oddName,
                                        OddValue = oddValue,
                                        SpecialBetValue = specialBetValue,
                                        BetId = betId,
                                    };

                                    bet.Odds.Add(odd);
                                }
                                match.Bets.Add(bet);
                            }
                            events.Matches.Add(match);
                        }
                        sport.Events.Add(events);
                    }
                    data.Sports.Add(sport);
                }
                data.SaveChanges();
            }
        }
    }
}
