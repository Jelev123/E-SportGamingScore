using E_SportGamingScore.Core.ViewModels.Matches;

namespace E_SportGamingScore.Core.Contracts.Matches
{
    public interface IMatches
    {
        IEnumerable<AllMatchesFor24H> AllMatches();

        GetMatchById GetMatchById(int matchId);
    }
}
