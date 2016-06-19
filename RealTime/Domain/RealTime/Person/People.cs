// --------------------------------------------------------------------------------------------------------------------
// <copyright file="People.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    public partial class People 
    {
        public Person FindByUsername(string username)
        {
            return this.FindBy(this.Meta.UserName, username);
        }

        protected override void RealTimeSecure(Security config)
        {
            var full = new[] { Operations.Read, Operations.Write, Operations.Execute };

            config.GrantAdministrator(this.ObjectType, full);
            config.GrantOwner(this.ObjectType, full);

            config.GrantMember(this.ObjectType, Operations.Read);
        }
    }
}