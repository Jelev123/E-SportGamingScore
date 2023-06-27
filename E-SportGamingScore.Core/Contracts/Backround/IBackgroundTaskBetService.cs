namespace E_SportGamingScore.Core.Contracts.Backround
{
    public interface IBackgroundTaskBetService
    {
        Task StartBackgroundTask(CancellationToken cancellationToken);

        void StopBackgroundTask();
    }
}
