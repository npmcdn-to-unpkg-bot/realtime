namespace Allors.Meta
{
    public partial class MetaPerson
    {
        private Tree mainTree;
        private Tree mainOnlineTree;
        private Tree appHomeTree;
        
        public Tree MainTree => this.mainTree ??
           (this.mainTree = new Tree(this)
               .Add(this.EndPoint, new Tree(M.EndPoint.Class)
                    .Add(M.EndPoint.Signals)
                    .Add(M.EndPoint.Call, new Tree(M.Call.Class)
                        .Add(M.Call.Initiator))));


        public Tree MainOnlineTree => this.mainOnlineTree ??
            (this.mainOnlineTree = new Tree(this)
               .Add(this.EndPoint, new Tree(M.EndPoint.Class)
                    .Add(M.EndPoint.Signals)
                    .Add(M.EndPoint.Call, new Tree(M.Call.Class)
                        .Add(M.Call.Initiator))));

        public Tree AppHomeTree => this.appHomeTree ??
            (this.appHomeTree = new Tree(this));
        
        internal override void RealTimeExtend()
        {
            
        }
    }
}