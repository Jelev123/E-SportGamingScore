using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace E_SportGamingScore.Infrastructure.Models
{
    public class Sport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [XmlAttribute("SportId")]
        public int SportId { get; set; }

        [XmlAttribute("Name")]
        public string SportName { get; set; }

        [XmlElement("Event")]
        public List<Event> Events { get; set; } = new List<Event>();
    }
}
