namespace E_SportGamingScore.Infrastructure.Models
{
    public class UpdateMatchMessages
    {
        public int Id { get; set; }

        public int MatchId { get; set; }

        public string MatchName { get; set; }

        public string MatchType { get; set; }

        public DateTime MatchStartDate { get; set; }

        public string ActionType { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
