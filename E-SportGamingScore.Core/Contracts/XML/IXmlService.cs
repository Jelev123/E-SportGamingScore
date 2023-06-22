namespace E_SportGamingScore.Core.Contracts.XML
{
    public interface IXmlService
    {
        Task ParseAndStoreData();

        Task Time(CancellationToken stoppingToken);
    }
}
