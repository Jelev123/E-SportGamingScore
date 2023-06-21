using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_SportGamingScore.Infrastructure.Models
{
    public class Sport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SportId { get; set; }

        public string SportName { get; set; }

        public ICollection<Event> Events { get; set; } = new HashSet<Event>();
    }
}
