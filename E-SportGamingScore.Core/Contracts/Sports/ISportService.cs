using E_SportGamingScore.Core.ViewModels.Sports;

namespace E_SportGamingScore.Core.Contracts.Sports
{
    public interface ISportService
    {
        Task<IEnumerable<SportsViewModel>> AllSports();
    }
}
