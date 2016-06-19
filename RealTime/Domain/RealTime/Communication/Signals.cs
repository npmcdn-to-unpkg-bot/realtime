namespace Allors.Domain
{
    public partial class Signals 
    {
        protected override void RealTimeSecure(Security config)
        {
            var full = new[] { Operations.Read, Operations.Write, Operations.Execute };

            config.GrantAdministrator(this.ObjectType, full);
            config.GrantOwner(this.ObjectType, full);

            config.GrantMember(this.ObjectType, Operations.Read);

            config.GrantCreator(this.ObjectType, full);
        }
    }
}