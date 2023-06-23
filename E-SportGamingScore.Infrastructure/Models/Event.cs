using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace E_SportGamingScore.Infrastructure.Models
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [XmlAttribute("EventId")]
        public int EventId { get; set; }

        [XmlAttribute("EventName")]
        public string EventName { get; set; }

        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlAttribute("CategoryId")]
        public int CategoryId { get; set; }

        [XmlAttribute("SportId")]
        public int SportId { get; set; }

        public Sport Sport { get; set; }

        [XmlElement("Match")]
        public ICollection<Match> Matches { get; set; } = new HashSet<Match>();
    }
}
