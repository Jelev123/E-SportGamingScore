using E_SportGamingScore.Core.Services.Matches;
using E_SportGamingScore.Core.ViewModels.Matches;
using E_SportGamingScore.Core.ViewModels.Ods;
using System.Timers;

namespace E_SportGamingScore.Core.Contracts.Matches
{
    public interface IMatchService
    {
        IEnumerable<AllMatchesFor24H> AllMatches();

        IEnumerable<AllMatchesFor24H> AllMatchesFor24H();

        GetMatchById GetMatchById(int matchId);

        Task CheckForChanges();
    }
}
