// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Roles.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System;

    public partial class UserGroups
    {
        public static readonly Guid MembersId = new Guid("8E1BC3C8-BB30-44F9-B028-43A399D64BFB");

        public UserGroup Members => this.Cache.Get(MembersId);

        protected override void RealTimeSetup(Setup setup)
        {
            new UserGroupBuilder(this.Session).WithName("Members").WithUniqueId(MembersId).Build();
        }
    }
}
