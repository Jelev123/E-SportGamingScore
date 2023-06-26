using E_SportGamingScore.Core.Contracts.BackGround;
using E_SportGamingScore.Core.Contracts.Matches;
using E_SportGamingScore.Core.Contracts.XML;

namespace E_SportGamingScore.Core.Services.XML
{
    public class BackgroundTaskXmlService : IBackgroundTaskXmlService
    {
        private Task _backgroundTask;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly IXmlService xmlService;

        public BackgroundTaskXmlService(IXmlService xmlService)
        {
            this.xmlService = xmlService;
        }

        public async Task StartBackgroundTask(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var linkedToken = _cancellationTokenSource.Token;

            _backgroundTask = Task.Run(async () =>
            {
                while (!linkedToken.IsCancellationRequested)
                {
                    var xmlAddTask = xmlService.ParseAndStoreData();
                    await Task.Delay(60000);
                }
            }, linkedToken);
        }

        public void StopBackgroundTask()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}
