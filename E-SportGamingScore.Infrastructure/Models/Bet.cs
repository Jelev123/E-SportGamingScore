using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace E_SportGamingScore.Infrastructure.Models
{
    public class Bet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [XmlAttribute("BetId")]
        public int BetId { get; set; }

        [XmlAttribute("BetName")]
        public string BetName { get; set; }

        [XmlAttribute("MatchId")]
        public int MatchId { get; set; }

        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        public Match Match { get; set; }

        [XmlElement("Odds")]
        public ICollection<Odd> Odds { get; set; } = new HashSet<Odd>();
    }
}
