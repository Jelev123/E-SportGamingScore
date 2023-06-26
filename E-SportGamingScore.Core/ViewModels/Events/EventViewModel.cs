using E_SportGamingScore.Core.ViewModels.Matches;

namespace E_SportGamingScore.Core.ViewModels.Events
{
    public class EventViewModel
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public bool IsLive { get; set; }

        public int CategoryId { get; set; }

        public List<AllMatchesFor24H> Matches { get; set; }
    }
}
