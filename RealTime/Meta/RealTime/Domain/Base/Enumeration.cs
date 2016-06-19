namespace Allors.Meta
{
    public partial class MetaEnumeration
    {
        internal override void RealTimeExtend()
        {
            this.Name.RelationType.AddGroup(Groups.Workspace);
        }
    }
}