using E_SportGamingScore.Core.ViewModels.Matches;

namespace E_SportGamingScore.Core.Contracts.Matches
{
    public interface IMatchService
    {
        Task<IEnumerable<AllMatchesFor24H>> AllMatches();

        Task<IEnumerable<AllMatchesFor24H>> AllMatchesFor24H();

        Task<GetMatchById> GetMatchById(int matchId);

        Task<IEnumerable<UpdateMatchesMessagesViewModel>> GenerateMatchUpdateMessages();

    }
}
