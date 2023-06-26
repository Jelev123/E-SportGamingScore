using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace E_SportGamingScore.Infrastructure.Models
{
    public class Match
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [XmlAttribute("MatchId")]
        public int MatchId { get; set; }

        [XmlAttribute("MatchName")]
        public string MatchName { get; set; }

        [XmlAttribute("StartDate")]
        public DateTime StartDate { get; set; }

        [XmlAttribute("MatchType")]
        public string MatchType { get; set; }

        [XmlAttribute("EventId")]
        public int EventId { get; set; }

        public Event Event { get; set; }

        [XmlElement("Bets")]
        public ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();
    }
}
