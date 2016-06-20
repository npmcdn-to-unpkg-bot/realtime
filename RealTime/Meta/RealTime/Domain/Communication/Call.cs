namespace Allors.Meta
{
    public partial class MetaCall
    {
        private Tree mainTree;
        
        public Tree MainTree => this.mainTree ??
           (this.mainTree = new Tree(this)
                .Add(this.Caller)
                .Add(this.Callee)
                .Add(this.CurrentObjectState));
        
        internal override void RealTimeExtend()
        {
        }
    }
}