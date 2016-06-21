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

        public void RealTimeWithdraw(CallWithdraw method)
        {
            this.CurrentObjectState = this.ObjectStates.Withdrawn;
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

        public void RealTimeOnPreDerive(ObjectOnPreDerive method)
        {
            var derivation = method.Derivation;

            derivation.AddDependency(this, this.Caller);
            derivation.AddDependency(this, this.Callee);
        }

        public void RealTimeOnDerive(ObjectOnDerive method)
        {
            var derivation = method.Derivation;

            // check if caller or callee only has 1 accepted call
            if (this.Caller.GetAllAcceptedCalls().Count > 1 || this.Callee.GetAllAcceptedCalls().Count > 1)
            {
                derivation.Validation.AddError(this, this.Meta.CurrentObjectState, "Only 1 Call can be in the Accepted State.");
            }

            if (!this.ExistStartDate && this.CurrentObjectState.IsAccepted)
            {
                this.StartDate = this.strategy.Session.Now();
            }

            if (!this.ExistEndDate && this.CurrentObjectState.IsEnded)
            {
                this.EndDate = this.strategy.Session.Now();
            }
        }
    }
}
