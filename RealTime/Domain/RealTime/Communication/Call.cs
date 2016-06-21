namespace Allors.Domain
{
    public partial class Call
    {
        public CallObjectStates ObjectStates => new CallObjectStates(this.Strategy.Session);

        ObjectState Transitional.CurrentObjectState => this.CurrentObjectState;

        public void RealTimeAccept(CallAccept method)
        {
            this.CurrentObjectState = this.ObjectStates.Accepted;
        }

        public void RealTimeReject(CallReject method)
        {
            this.CurrentObjectState = this.ObjectStates.Rejected;
        }

        public void RealTimeEnd(CallEnd method)
        {
            this.CurrentObjectState = this.ObjectStates.Ended;
        }

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
            var derivation = method.Derivation;
        }
    }
}
