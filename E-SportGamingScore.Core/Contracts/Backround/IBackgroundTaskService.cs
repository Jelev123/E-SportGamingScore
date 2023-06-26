namespace E_SportGamingScore.Core.Contracts.BackGround
{
    public interface IBackgroundTaskService
    {
        Task StartBackgroundTask(CancellationToken cancellationToken);

        void StopBackgroundTask();

        Task XmlAdd();

        Task CheckForChange();
    }
}
