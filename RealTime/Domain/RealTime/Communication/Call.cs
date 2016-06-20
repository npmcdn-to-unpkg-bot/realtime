namespace Allors.Domain
{
    public partial class Call
    {
        public CallObjectStates ObjectStates => new CallObjectStates(this.Strategy.Session);

        ObjectState Transitional.CurrentObjectState => this.CurrentObjectState;

        public void RealTimeOnBuild(ObjectOnBuild method)
        {
            if (!this.ExistCreationDate)
            {
                this.CreationDate = this.strategy.Session.Now();
            }

            if (!this.ExistCurrentObjectState)
            {
                this.CurrentObjectState = new CallObjectStates(this.strategy.Session).Requested;
            }
        }

        public void RealTimeOnDerive(ObjectOnDerive method)
        {
        }
    }
}
