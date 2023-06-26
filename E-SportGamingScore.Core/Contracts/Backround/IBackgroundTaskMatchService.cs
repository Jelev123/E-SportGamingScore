namespace E_SportGamingScore.Core.Contracts.Backround
{
    public interface IBackgroundTaskMatchService
    {
        Task StartBackgroundTask(CancellationToken cancellationToken);

        void StopBackgroundTask();
    }
}
