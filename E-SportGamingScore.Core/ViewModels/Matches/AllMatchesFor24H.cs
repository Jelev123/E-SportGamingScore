using E_SportGamingScore.Core.ViewModels.Ods;
using System.Collections.Generic;

namespace E_SportGamingScore.Core.ViewModels.Matches
{
    public class AllMatchesFor24H
    {
        public int Id { get; set; }

        public int MatchId { get; set; }

        public string MatchName { get; set; }

        public string MatchType { get; set; }

        public DateTime MatchStartDate { get; set; }

        public string BetNames { get; set; }

        public bool IsBerAlive { get; set; }

        public List<OdsViewModel> Odds { get; set; }

    }
}
