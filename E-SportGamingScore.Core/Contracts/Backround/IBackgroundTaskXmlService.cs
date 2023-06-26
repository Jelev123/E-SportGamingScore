namespace E_SportGamingScore.Core.Contracts.BackGround
{
    public interface IBackgroundTaskXmlService
    {
        Task StartBackgroundTask(CancellationToken cancellationToken);

        void StopBackgroundTask();

    }
}
