namespace E_SportGamingScore.Infrastructure.Models
{
    public class Event
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public bool IsLive { get; set; }

        public int SportId { get; set; }

        public Sport Sport { get; set; }

        public ICollection<Match> Matches { get; set; } = new HashSet<Match>();
    }
}
