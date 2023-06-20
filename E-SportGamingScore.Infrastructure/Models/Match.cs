namespace E_SportGamingScore.Infrastructure.Models
{
    public class Match
    {
        public int MatchId { get; set; }

        public string MatchName { get; set; }

        public DateTime StartDate { get; set; }

        public string MatchType { get; set; }

        public bool IsLive { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();
    }
}
