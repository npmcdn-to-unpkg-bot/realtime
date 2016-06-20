namespace Allors.Meta
{
    public partial class MetaPerson
    {
        private Tree mainTree;
        private Tree mainOnlineTree;
        private Tree appHomeTree;
        
        public Tree MainTree => this.mainTree ??
           (this.mainTree = new Tree(this));
        
        public Tree MainOnlineTree => this.mainOnlineTree ??
            (this.mainOnlineTree = new Tree(this));

        public Tree AppHomeTree => this.appHomeTree ??
            (this.appHomeTree = new Tree(this));
        
        internal override void RealTimeExtend()
        {
            
        }
    }
}