using E_SportGamingScore.Core.ViewModels.Matches;
using E_SportGamingScore.Core.ViewModels.Ods;

namespace E_SportGamingScore.Core.Contracts.Matches
{
    public interface IMatches
    {
        IEnumerable<AllMatchesFor24H> AllMatches();

        IEnumerable<AllMatchesFor24H> AllMatchesFor24H();

        GetMatchById GetMatchById(int matchId);

        void CheckFroChanges();
    }
}
