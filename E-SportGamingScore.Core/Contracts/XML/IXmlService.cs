using Microsoft.AspNetCore.Http;
using System.Xml;

namespace E_SportGamingScore.Core.Contracts.XML
{
    public interface IXmlService
    {
        Task<XmlDocument> ReadXml(string xmlPath);

        Task ParseAndStoreData();

        Task Time(CancellationToken stoppingToken);
    }
}
