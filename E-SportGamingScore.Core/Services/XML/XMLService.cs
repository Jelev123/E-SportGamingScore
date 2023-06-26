using E_SportGamingScore.Core.Constants;
using E_SportGamingScore.Core.Contracts.XML;
using E_SportGamingScore.Infrastructure.Data;
using E_SportGamingScore.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace E_SportGamingScore.Core.Services.XML
{
    public class XMLService : IXmlService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public XMLService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<XmlDocument> ReadXml(string xmlPath)
        {
            try
            {
                
                XmlSerializer serializer = new XmlSerializer(typeof(XmlDocument));
                using (StreamReader reader = new StreamReader(xmlPath, detectEncodingFromByteOrderMarks: true))
                {
                    XmlDocument xmlDoc = (XmlDocument)serializer.Deserialize(reader);
                    return xmlDoc;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ParseAndStoreData()
        {

            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var xmlFilePath = XmlConstants.xmlFilePath;
                    var xmlDoc = await this.ReadXml(xmlFilePath);
                    XmlNodeList sportNodes = xmlDoc.GetElementsByTagName("Sport");

                    foreach (XmlNode sportNode in sportNodes)
                    {
                        var sportId = int.Parse(sportNode.Attributes["ID"].Value);
                        var sportName = sportNode.Attributes["Name"].Value;

                        var existingSport = context.Sports.FirstOrDefault(s => s.SportId == sportId);

                        if (existingSport == null)
                        {
                            Sport sport = new Sport
                            {
                                SportId = sportId,
                                SportName = sportName,
                            };

                            context.Sports.Add(sport);
                        }

                        XmlNodeList eventNodes = sportNode.SelectNodes("Event");
                        foreach (XmlNode eventNode in eventNodes)
                        {
                            var eventId = int.Parse(eventNode.Attributes["ID"].Value);
                            var eventName = eventNode.Attributes["Name"].Value;
                            var categoryIdString = eventNode.Attributes["CategoryID"].Value;
                            var categoryId = int.Parse(categoryIdString);
                            var isEventLive = bool.Parse(eventNode.Attributes["IsLive"].Value);

                            var existingEvent = context.Events.FirstOrDefault(e => e.EventId == eventId);

                            if (existingEvent == null)
                            {
                                Event events = new Event
                                {
                                    EventId = eventId,
                                    EventName = eventName,
                                    IsLive = isEventLive,
                                    CategoryId = categoryId,
                                    SportId = sportId
                                };

                                context.Events.Add(events);
                            }

                            XmlNodeList matchNodes = eventNode.SelectNodes("Match");
                            foreach (XmlNode matchNode in matchNodes)
                            {
                                var matchId = int.Parse(matchNode.Attributes["ID"].Value);
                                var matchName = matchNode.Attributes["Name"].Value;
                                var matchTtpe = matchNode.Attributes["MatchType"].Value;
                                var startDate = DateTime.Parse(matchNode.Attributes["StartDate"].Value);

                                var existingMatch = context.Matches.FirstOrDefault(m => m.MatchId == matchId);

                                if (existingMatch == null)
                                {
                                    Match match = new Match
                                    {
                                        MatchId = matchId,
                                        MatchName = matchName,
                                        MatchType = matchTtpe,
                                        StartDate = startDate,
                                        EventId = eventId
                                    };

                                    context.Matches.Add(match);
                                }

                                XmlNodeList betNodes = matchNode.SelectNodes("Bet");
                                foreach (XmlNode betNode in betNodes)
                                {
                                    var betId = int.Parse(betNode.Attributes["ID"].Value);
                                    var betName = betNode.Attributes["Name"].Value;
                                    var betIsLive = bool.Parse(betNode.Attributes["IsLive"].Value);

                                    var existingBet = context.Bets.FirstOrDefault(m => m.BetId == betId);

                                    if (existingBet == null)
                                    {
                                        Bet bet = new Bet
                                        {
                                            BetId = betId,
                                            BetName = betName,
                                            IsLive = betIsLive,
                                            MatchId = matchId,
                                        };

                                        context.Bets.Add(bet);
                                    }

                                    XmlNodeList oddsNodes = betNode.SelectNodes("Odd");
                                    foreach (XmlNode oddNode in oddsNodes)
                                    {
                                        var oddId = int.Parse(oddNode.Attributes["ID"]?.Value);
                                        var oddName = oddNode.Attributes["Name"].Value;
                                        var oddValue = decimal.Parse(oddNode.Attributes["Value"]?.Value, CultureInfo.InvariantCulture);
                                        decimal? specialBetValue = !string.IsNullOrEmpty(oddNode.Attributes["SpecialBetValue"]?.Value)
                                        ? decimal.Parse(oddNode.Attributes["SpecialBetValue"]?.Value, CultureInfo.InvariantCulture)
                                        : (decimal?)null;

                                        var existingOdd = context.Odds.FirstOrDefault(m => m.OddId == oddId);

                                        if (existingOdd == null)
                                        {
                                            Odd odd = new Odd
                                            {
                                                OddId = oddId,
                                                Name = oddName,
                                                OddValue = oddValue,
                                                SpecialBetValue = specialBetValue,
                                                BetId = betId,

                                            };

                                            context.Odds.Add(odd);
                                        }
                                    }
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
        }
    }
}
