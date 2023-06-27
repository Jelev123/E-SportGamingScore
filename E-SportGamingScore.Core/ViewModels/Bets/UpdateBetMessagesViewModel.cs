namespace E_SportGamingScore.Core.ViewModels.Bets
{
    public class UpdateBetMessagesViewModel
    {
        public int Id { get; set; }

        public int BetId { get; set; }

        public string BetName { get; set; }

        public bool IsBetLive { get; set; }

        public string MessageType { get; set; }

        public int EntityId { get; set; }

        public string ActionType { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
