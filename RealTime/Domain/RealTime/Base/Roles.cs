// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Roles.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System;

    public partial class Roles
    {
        public static readonly Guid MemberId = new Guid("8BD2B2B7-7806-4ED7-A7AC-DB776FF10358");

        public Role Member => this.RoleCache.Get(MemberId);

        protected override void RealTimeSetup(Setup setup)
        {
            new RoleBuilder(this.Session).WithName("Member").WithUniqueId(MemberId).Build();
        }
    }
}
