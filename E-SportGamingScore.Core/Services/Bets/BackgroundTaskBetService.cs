using E_SportGamingScore.Core.Contracts.Backround;
using E_SportGamingScore.Core.Contracts.Bet;

namespace E_SportGamingScore.Core.Services.Bets
{
    public class BackgroundTaskBetService : IBackgroundTaskBetService
    {
        private readonly IBetService betService;
        private Task _backgroundTask;
        private CancellationTokenSource _cancellationTokenSource;

        public BackgroundTaskBetService(IBetService betService)
        {
            this.betService = betService;
        }


        public async Task StartBackgroundTask(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var linkedToken = _cancellationTokenSource.Token;

            _backgroundTask = Task.Run(async () =>
            {
                while (!linkedToken.IsCancellationRequested)
                {
                    var checkForChangeTask = this.betService.GenerateBetUpdateMessages();
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
