namespace Allors.Meta
{
    public partial class MetaEndPoint
    {
        private Tree mainTree;
        
        public Tree MainTree => this.mainTree ?? 
            (this.mainTree = new Tree(this)
                .Add(M.EndPoint.Signals));
        
        internal override void RealTimeExtend()
        {
        }
    }
}