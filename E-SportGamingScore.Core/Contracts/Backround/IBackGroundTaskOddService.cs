namespace E_SportGamingScore.Core.Contracts.Backround
{
    public interface IBackGroundTaskOddService
    {
        Task StartBackgroundTask(CancellationToken cancellationToken);

        void StopBackgroundTask();
    }
}
