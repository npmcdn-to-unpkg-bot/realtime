namespace Allors.Meta
{
    public partial class MetaPerson
    {
        private Tree mainTree;
        private Tree mainOnlineTree;
        private Tree appHomeTree;
        
        public Tree MainTree => this.mainTree ??
           (this.mainTree = new Tree(this)
                .Add(this.AcceptedCall, 
                    new Tree(M.Call)
                        .Add(M.Call.CurrentObjectState)
                        .Add(M.Call.Caller)
                        .Add(M.Call.Callee)));
        
        public Tree AppHomeTree => this.appHomeTree ??
            (this.appHomeTree = new Tree(this)
                .Add(this.RequestedCalls)
                .Add(this.AcceptedCall));
        
        internal override void RealTimeExtend()
        {
            
        }
    }
}