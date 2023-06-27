namespace E_SportGamingScore.Core.Services.Background
{
    public interface IBackgroundTaskOddService
    {
        Task StartBackgroundTask(CancellationToken cancellationToken);

        void StopBackgroundTask();
    }
}
