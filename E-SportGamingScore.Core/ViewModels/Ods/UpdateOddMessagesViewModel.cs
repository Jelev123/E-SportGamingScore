namespace E_SportGamingScore.Core.ViewModels.Ods
{
    public class UpdateOddMessagesViewModel
    {
        public int Id { get; set; }
        public int OddId { get; set; }
        public string OddName { get; set; }

        public decimal OddValue { get; set; }

        public decimal SpecialBetValue { get; set; }

        public string MessageType { get; set; }

        public int EntityId { get; set; }

        public string ActionType { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
