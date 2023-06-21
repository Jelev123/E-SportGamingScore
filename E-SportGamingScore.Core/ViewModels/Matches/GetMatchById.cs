using E_SportGamingScore.Core.ViewModels.Ods;

namespace E_SportGamingScore.Core.ViewModels.Matches
{
    public class GetMatchById
    {
        public int MatchId { get; set; }

        public string MatchName { get; set; }

        public List<OdsViewModel> BetName { get; set; }

        public DateTime StartDate { get; set; }

        public List<OdsViewModel> Ods { get; set; }
    }
}
