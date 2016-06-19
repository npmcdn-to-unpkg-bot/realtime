namespace Allors.Meta
{
    public partial class MetaUser
    {
        internal override void RealTimeExtend()
        {
            this.UserName.RelationType.AddGroup(Groups.Workspace);
            this.UserEmail.RelationType.AddGroup(Groups.Workspace);
        }
    }
}