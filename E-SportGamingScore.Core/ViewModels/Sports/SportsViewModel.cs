using E_SportGamingScore.Core.ViewModels.Events;

namespace E_SportGamingScore.Core.ViewModels.Sports
{
    public class SportsViewModel
    {
        public int SportId { get; set; }

        public string SportName { get; set; }

        public List<EventViewModel> Events { get; set; }
    }
}
