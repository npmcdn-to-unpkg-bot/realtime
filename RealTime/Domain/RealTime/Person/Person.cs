// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System.Collections.Generic;

    public partial class Person
    {
        public void RealTimeOnBuild(ObjectOnBuild method)
        {
            new UserGroups(this.strategy.Session).Creators.AddMember(this);
            new UserGroups(this.strategy.Session).Members.AddMember(this);
        }
        
        public void RealTimeOnDerive(ObjectOnDerive method)
        {
            this.DeriveSecurity();
        }

        public override string ToString()
        {
            return this.UserName;
        }

        private void DeriveSecurity()
        {
            var tokens = new List<SecurityToken>()
            {
                this.OwnerSecurityToken,
                Singleton.Instance(this.strategy.Session).DefaultSecurityToken
            };

            this.SecurityTokens = tokens.ToArray();
        }
    }
}
