// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Singletons.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    public partial class Singletons
    {
        protected override void RealTimeSetup(Setup config)
        {
            var singleton = this.Instance;

            // Member
            singleton.DefaultMembersAccessControl = new AccessControlBuilder(this.Session)
                .WithRole(new Roles(this.Session).Member)
                .WithSubjectGroup(new UserGroups(this.Session).Members)
                .Build();

            singleton.DefaultSecurityToken.AddAccessControl(singleton.DefaultMembersAccessControl);
        }

        protected override void RealTimeSecure(Security config)
        {
            config.GrantMember(this.ObjectType, Operations.Read);
        }
    }
}
