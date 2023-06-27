using E_SportGamingScore.Core.ViewModels.Bets;
using E_SportGamingScore.Core.ViewModels.Ods;

namespace E_SportGamingScore.Core.ViewModels.Matches
{
    public class GetMatchById
    {
        public int MatchId { get; set; }

        public string MatchName { get; set; }

        public string MatchType { get; set; }

        public DateTime StartDate { get; set; }

        public List<BetViewModel> ActiveMarkets { get; set; }

        public List<BetViewModel> InactiveMarkets { get; set; }
    }
}
