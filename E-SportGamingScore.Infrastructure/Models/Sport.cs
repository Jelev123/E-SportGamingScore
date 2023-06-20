namespace E_SportGamingScore.Infrastructure.Models
{
    public class Sport
    {
        public int SportId { get; set; }

        public string SportName { get; set; }

        public ICollection<Event> Events { get; set; } = new HashSet<Event>();
    }
}
