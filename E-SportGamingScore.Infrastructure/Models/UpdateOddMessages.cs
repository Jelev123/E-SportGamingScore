namespace E_SportGamingScore.Infrastructure.Models
{
    public class UpdateOddMessages
    {
        public int Id { get; set; }

        public int OddId { get; set; }

        public string OddName { get; set; }

        public decimal OddValue { get; set; }

        public decimal? SpecialBetValue { get; set; }

        public string ActionType { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
