namespace E_SportGamingScore.Infrastructure.Models
{
    public class Bet
    {
        public int BetId { get; set; }

        public string BetName { get; set; }

        public int MatchId { get; set; }

        public bool IsLive { get; set; }

        public Match Match { get; set; }

        public ICollection<Odd> Odds { get; set; } = new HashSet<Odd>();
    }
}
