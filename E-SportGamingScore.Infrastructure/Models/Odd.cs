using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace E_SportGamingScore.Infrastructure.Models
{
    public class Odd
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [XmlAttribute("OddId")]
        public int OddId { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Value")]
        public decimal OddValue { get; set; }

        [XmlAttribute("SpecialBetValue")]
        public decimal? SpecialBetValue { get; set; }

        [XmlAttribute("BetId")]
        public int BetId { get; set; }

        public Bet Bet { get; set; }
    }
}
