namespace Allors.Meta
{
    public partial class MetaUniquelyIdentifiable
    {
        internal override void RealTimeExtend()
        {
            this.UniqueId.RelationType.AddGroup(Groups.Workspace);
        }
    }
}