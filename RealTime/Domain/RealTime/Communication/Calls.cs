namespace Allors.Domain
{
    public partial class Calls 
    {
        protected override void RealTimeSecure(Security config)
        {
            var full = new[] { Operations.Read, Operations.Write, Operations.Execute };

            config.GrantAdministrator(this.ObjectType, full);
            config.GrantOwner(this.ObjectType, full);

            config.GrantMember(this.ObjectType, full);

            config.GrantCreator(this.ObjectType, full);
        }
    }
}