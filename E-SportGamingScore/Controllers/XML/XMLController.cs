using E_SportGamingScore.Core.Contracts.XML;
using Microsoft.AspNetCore.Mvc;

namespace E_SportGamingScore.Controllers.XML
{
    public class XMLController : Controller
    {
        private readonly IXmlService xmlService;

        public XMLController(IXmlService xmlService)
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
