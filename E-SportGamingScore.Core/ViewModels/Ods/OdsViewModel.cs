namespace E_SportGamingScore.Core.ViewModels.Ods
{
    public class OdsViewModel
    {
        public int OddId { get; set; }
        public string OddName { get; set; }

        public string BetName { get; set; }

        public int BetId { get; set; }

        public decimal OddValue { get; set; }

        public decimal? SpecialBetValue { get; set; }
    }
}
