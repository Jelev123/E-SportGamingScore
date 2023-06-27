using E_SportGamingScore.Core.Contracts.Backround;
using E_SportGamingScore.Core.Contracts.Matches;

namespace E_SportGamingScore.Core.Services.Matches
{
    public class BackgroundTaskMatchesService : IBackgroundTaskService
    {
        private readonly IMatchService matchService;
        private Task _backgroundTask;
        private CancellationTokenSource _cancellationTokenSource;

        public BackgroundTaskMatchesService(IMatchService mathService)
        {
            this.matchService = mathService;
        }

        public async Task StartBackgroundTask(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var linkedToken = _cancellationTokenSource.Token;

            _backgroundTask = Task.Run(async () =>
            {
                while (!linkedToken.IsCancellationRequested)
                {
                    var checkForChangeTask = this.matchService.GenerateMatchUpdateMessages();
                    await Task.Delay(10000);
                }
            }, linkedToken);
        }

        public void StopBackgroundTask()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}
