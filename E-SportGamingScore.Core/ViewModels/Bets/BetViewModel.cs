using E_SportGamingScore.Core.ViewModels.Ods;

namespace E_SportGamingScore.Core.ViewModels.Bets
{
    public class BetViewModel
    {
        public string BetName { get; set; }

        public int BetId { get; set; }

        public int MatchId { get; set; }

        public bool IsBetLive { get; set; }

        public List<OdsViewModel> Odds { get; set; }
    }
}
