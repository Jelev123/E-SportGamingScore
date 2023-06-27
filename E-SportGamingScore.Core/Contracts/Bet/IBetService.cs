using E_SportGamingScore.Core.ViewModels.Bets;

namespace E_SportGamingScore.Core.Contracts.Bet
{
    public interface IBetService
    {
        Task<IEnumerable<UpdateBetMessagesViewModel>> GenerateBetUpdateMessages();
    }
}
