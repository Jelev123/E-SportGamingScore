using E_SportGamingScore.Core.Contracts.XML;
using Microsoft.AspNetCore.Mvc;

namespace E_SportGamingScore.Controllers.XML
{
    public class XMLController : Controller
    {
        private readonly IXML xmlService;

        public XMLController(IXML xmlService)
        {
            this.xmlService = xmlService;
        }

        [HttpPost]
        [Route("/process-xml")]
        public IActionResult ProcessXml()
        {
                xmlService.ParseAndStoreData();
                return this.Redirect("/");
        }
    }
}
