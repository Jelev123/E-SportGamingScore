namespace E_SportGamingScore.Infrastructure.Models
{
    public class UpdateBetMessages
    {
        public int Id { get; set; }

        public int BetId { get; set; }

        public string BetName { get; set; }

        public bool IsBetLive { get; set; }

        public string ActionType { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
