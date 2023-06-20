namespace E_SportGamingScore.Infrastructure.Models
{
    public class Odd
    {
        public int OddId { get; set; }

        public decimal OddValue { get; set; }

        public int BetId { get; set; }

        public Bet Bet { get; set; }
    }
}
