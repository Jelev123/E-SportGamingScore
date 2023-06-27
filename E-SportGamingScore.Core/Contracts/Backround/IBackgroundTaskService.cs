namespace E_SportGamingScore.Core.Contracts.Backround
{
    public interface IBackgroundTaskService
    {
        Task StartBackgroundTask(CancellationToken cancellationToken);

        void StopBackgroundTask();
    }
}
