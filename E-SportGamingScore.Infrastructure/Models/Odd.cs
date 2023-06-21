using System.ComponentModel.DataAnnotations.Schema;

namespace E_SportGamingScore.Infrastructure.Models
{
    public class Odd
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OddId { get; set; }

        public string Name { get; set; }

        public decimal OddValue { get; set; }

        public decimal? SpecialBetValue { get; set; }

        public int BetId { get; set; }

        public Bet Bet { get; set; }
    }
}
