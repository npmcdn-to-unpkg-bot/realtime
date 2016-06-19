// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Security.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using Allors.Meta;

    public partial class Security
    {
        public void GrantMember(ObjectType objectType, params Operations[] operations)
        {
            this.Grant(Roles.MemberId, objectType, operations);
        }
     }
}