using Microsoft.AspNetCore.Http;

namespace E_SportGamingScore.Core.Contracts.XML
{
    public interface IXmlService
    {
        Task ParseAndStoreData(IServiceProvider serviceProvider);

        Task Time(CancellationToken stoppingToken);
    }
}
