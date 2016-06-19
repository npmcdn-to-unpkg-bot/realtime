namespace Allors
{
    using Allors.Domain;

    public class AsyncScheduler : Scheduler
    {
        public override void Schedule()
        {
            var database = this.SnapshotDatabase;

            this.Logger.Info("Derive Async");
            AsyncDerivations.AsyncDerive(database);
        }
    }
}
