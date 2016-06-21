// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System.Collections.Generic;
    using System.Linq;

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

            var requestedCalls = new List<Call>();
            requestedCalls.AddRange(this.CallsWhereCaller.Where(v => v.CurrentObjectState.IsRequested));
            requestedCalls.AddRange(this.CallsWhereCallee.Where(v => v.CurrentObjectState.IsRequested));
            this.RequestedCalls = requestedCalls.ToArray();

            var acceptedCalls = this.GetAllAcceptedCalls();
            this.AcceptedCall = acceptedCalls.FirstOrDefault();
        }

        public override string ToString()
        {
            return this.UserName;
        }

        internal HashSet<Call> GetAllAcceptedCalls()
        {
            var acceptedCalls = new HashSet<Call>();
            acceptedCalls.UnionWith(this.CallsWhereCaller.Where(v => v.CurrentObjectState.IsAccepted));
            acceptedCalls.UnionWith(this.CallsWhereCallee.Where(v => v.CurrentObjectState.IsAccepted));
            return acceptedCalls;
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
