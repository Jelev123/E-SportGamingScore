using E_SportGamingScore.Core.ViewModels.Bets;
using E_SportGamingScore.Core.ViewModels.Ods;
using System.Collections.Generic;

namespace E_SportGamingScore.Core.ViewModels.Matches
{
    public class AllMatchesFor24H
    {
        public int MatchId { get; set; }

        public string MatchName { get; set; }

        public string MatchType { get; set; }

        public DateTime MatchStartDate { get; set; }

        public bool IsBerAlive { get; set; }

        public int EventId { get; set; }

        public List<BetViewModel> Bets { get; set; }

    }
}
