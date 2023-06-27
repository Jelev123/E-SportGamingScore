using E_SportGamingScore.Core.ViewModels.Ods;

namespace E_SportGamingScore.Core.Contracts.Odd
{
    public interface IOddService
    {
        Task<IEnumerable<UpdateOddMessagesViewModel>> GenerateOddUpdateMessages();
    }
}
