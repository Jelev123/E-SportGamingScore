namespace E_SportGamingScore.Core.ViewModels.Matches
{
    public class UpdateMatchesMessagesViewModel
    {
        public int Id { get; set; }

        public int MatchId { get; set; }

        public string MatchName { get; set; }

        public string MatchType { get; set; }

        public DateTime MatchStartDate { get; set; }

        public string MessageType { get; set; }

        public int EntityId { get; set; }

        public string ActionType { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
