using E_SportGamingScore.Core.Contracts.Bet;
using E_SportGamingScore.Core.Contracts.Sports;
using E_SportGamingScore.Core.Enums;
using E_SportGamingScore.Core.ViewModels.Bets;
using E_SportGamingScore.Core.ViewModels.Ods;
using E_SportGamingScore.Infrastructure.Data;
using E_SportGamingScore.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;

namespace E_SportGamingScore.Core.Services.Bets
{
    public class BetService : IBetService
    {
        private readonly ISportService sportService;
        private readonly IServiceScopeFactory serviceScopeFactory;


        public BetService(ISportService sportService, IServiceScopeFactory serviceScopeFactory)
        {
            this.sportService = sportService;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<IEnumerable<UpdateBetMessagesViewModel>> GenerateBetUpdateMessages()
        {
            var updatedBets = new List<UpdateBetMessages>();
            var testUpdateBetMessages = new List<UpdateBetMessagesViewModel>();
            var currentSports = this.sportService.AllSports();

            testUpdateBetMessages.Add(new UpdateBetMessagesViewModel
            {
                BetId = 47516460,
                BetName = "bet 1",
                IsBetLive = true,
                ActionType = MessageActionTypeEnum.Update.ToString(),
                TimeStamp = DateTime.Now
            }); testUpdateBetMessages.Add(new UpdateBetMessagesViewModel
            {
                BetId = 47516460,
                BetName = "bet 2",
                IsBetLive = false,
                EntityId = 1,
                ActionType = MessageActionTypeEnum.Update.ToString(),
                TimeStamp = DateTime.Now
            });

            var uniqueUpdateBetMessages = testUpdateBetMessages
             .GroupBy(u => new { u.BetId })
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

                                    var matchingUpdateBet = uniqueUpdateBetMessages.FirstOrDefault(
                                            u => u.BetId == currentBet.BetId);

                                    if (matchingUpdateBet != null)
                                    {
                                        currentBet.BetName = matchingUpdateBet.BetName;
                                        currentBet.IsBetLive = matchingUpdateBet.IsBetLive;

                                        var update = new UpdateBetMessages
                                        {
                                            BetId = currentBet.BetId,
                                            BetName = currentBet.BetName,
                                            IsBetLive = currentBet.IsBetLive,
                                            ActionType = MessageActionTypeEnum.Update.ToString(),
                                            TimeStamp = DateTime.Now
                                        };

                                        updatedBets.Add(update);
                                        context.UpdateBetMessages.Add(update);
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

            return (IEnumerable<UpdateBetMessagesViewModel>)updatedBets;
        }
    }
}
