namespace Allors.Meta
{
    public partial class MetaDeletable
    {
        internal override void RealTimeExtend()
        {
            this.Delete.AddGroup(Groups.Workspace);
        }
    }
}