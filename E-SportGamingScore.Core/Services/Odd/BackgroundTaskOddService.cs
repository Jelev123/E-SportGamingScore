using E_SportGamingScore.Core.Contracts.Backround;
using E_SportGamingScore.Core.Contracts.Odd;

namespace E_SportGamingScore.Core.Services.Odd
{
    public class BackgroundTaskOddService : IBackgroundTaskService
    {
        private readonly IOddService oddService;
        private Task _backgroundTask;
        private CancellationTokenSource _cancellationTokenSource;

        public BackgroundTaskOddService(IOddService oddService)
        {
            this.oddService = oddService;
        }


        public async Task StartBackgroundTask(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var linkedToken = _cancellationTokenSource.Token;

            _backgroundTask = Task.Run(async () =>
            {
                while (!linkedToken.IsCancellationRequested)
                {
                    var checkForChangeTask = this.oddService.GenerateOddUpdateMessages();
                    await Task.Delay(5000);
                }
            }, linkedToken);
        }

        public void StopBackgroundTask()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}
