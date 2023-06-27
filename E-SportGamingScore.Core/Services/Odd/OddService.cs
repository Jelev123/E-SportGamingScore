using E_SportGamingScore.Core.Contracts.Odd;
using E_SportGamingScore.Core.Contracts.Sports;
using E_SportGamingScore.Core.Enums;
using E_SportGamingScore.Core.ViewModels.Ods;
using E_SportGamingScore.Infrastructure.Data;
using E_SportGamingScore.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;

namespace E_SportGamingScore.Core.Services.Odd
{
    public class OddService : IOddService
    {
        private readonly ISportService sportService;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public OddService(ISportService sportService, IServiceScopeFactory serviceScopeFactory)
        {
            this.sportService = sportService;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<IEnumerable<UpdateOddMessagesViewModel>> GenerateOddUpdateMessages()
        {
            var updatedOdds = new List<UpdateOddMessages>();
            var testUpdateOddMessages = new List<UpdateOddMessagesViewModel>();
            var currentSports = this.sportService.AllSports();


            testUpdateOddMessages.Add(new UpdateOddMessagesViewModel
            {
                OddId = 327115211,
                OddName = "Odd 1",
                OddValue = 1.5m,
                SpecialBetValue = 0.75m,
                ActionType = MessageActionTypeEnum.Update.ToString(),
                TimeStamp = DateTime.Now
            });  testUpdateOddMessages.Add(new UpdateOddMessagesViewModel
            {
                OddId = 327115212,
                OddName = "Odd 2",
                OddValue = 10.5m,
                SpecialBetValue = 5.75m,
                ActionType = MessageActionTypeEnum.Update.ToString(),
                TimeStamp = DateTime.Now
            });

            var uniqueUpdateOddMessages = testUpdateOddMessages
             .GroupBy(u => new { u.OddId, u.OddName, u.OddValue, u.SpecialBetValue })
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
                                foreach (var currentBet in currentMatch.Bets)
                                {
                                    foreach (var currentOdd in currentBet.Odds)
                                    {

                                        var matchingUpdateOdd = uniqueUpdateOddMessages.FirstOrDefault(
                                                u => u.OddId == currentOdd.OddId);

                                        if (matchingUpdateOdd != null)
                                        {
                                            currentOdd.OddName = matchingUpdateOdd.OddName;
                                            currentOdd.OddValue = matchingUpdateOdd.OddValue;
                                            currentOdd.SpecialBetValue = matchingUpdateOdd.SpecialBetValue;

                                            var update = new UpdateOddMessages
                                            {
                                                OddId = currentOdd.OddId,
                                                OddName = currentOdd.OddName,
                                                OddValue = currentOdd.OddValue,
                                                SpecialBetValue = currentOdd.SpecialBetValue,
                                                ActionType = MessageActionTypeEnum.Update.ToString(),
                                                TimeStamp = DateTime.Now
                                            };

                                            updatedOdds.Add(update);
                                            context.UpdateOddMessages.Add(update);
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



            return (IEnumerable<UpdateOddMessagesViewModel>)updatedOdds;
        }

    }
}
