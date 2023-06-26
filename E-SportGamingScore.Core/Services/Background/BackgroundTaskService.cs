using E_SportGamingScore.Core.Contracts.BackGround;
using E_SportGamingScore.Core.Contracts.Matches;
using E_SportGamingScore.Core.Contracts.XML;

namespace E_SportGamingScore.Core.Services.Background
{
    public class BackgroundTaskService : IBackgroundTaskService
    {
        private Task _backgroundTask;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly IXmlService xmlService;
        private readonly IMatchService matchService;

        public BackgroundTaskService(IXmlService xmlService, IMatchService matchService)
        {
            this.xmlService = xmlService;
            this.matchService = matchService;
        }

        public async Task StartBackgroundTask(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var linkedToken = _cancellationTokenSource.Token;

            _backgroundTask = Task.Run(async () =>
            {
                while (!linkedToken.IsCancellationRequested)
                {
                    var checkForChangeTask = this.CheckForChange();
                    var xmlAddTask = this.XmlAdd();

                    await Task.WhenAll(xmlAddTask, checkForChangeTask);
                }
            }, linkedToken);
        }

        public void StopBackgroundTask()
        {
            _cancellationTokenSource?.Cancel();
        }

        public async Task XmlAdd()
        {
            await this.xmlService.ParseAndStoreData();
            await Task.Delay(1000);
        }

        public async Task CheckForChange()
        {
            await this.matchService.CheckForChanges();
            await Task.Delay(5000);
        }
    }
}
