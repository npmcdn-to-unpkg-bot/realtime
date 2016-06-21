// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Communication.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System;

    public partial class CallObjectStates
    {
        //public static readonly Guid NoneId = new Guid("E042A3BA-D399-4A08-B76A-9F5A411DF4C9");
        public static readonly Guid RequestedId = new Guid("4706152D-6CA8-4BC7-880D-981F2739CD26");
        public static readonly Guid AcceptedId = new Guid("A95FFD0F-113E-483B-9B37-CF56371AC518");
        public static readonly Guid WithdrawnId = new Guid("5E3760D1-A45A-4BDC-BF7A-B059EEAE1347");
        public static readonly Guid RejectedId = new Guid("FFA6492D-C648-488A-9D0B-2767FED7CD33");
        public static readonly Guid EndedId = new Guid("78BDF595-9C16-46A5-81A7-460F2FB163FF");

        private UniquelyIdentifiableCache<CallObjectState> cache;

        public CallObjectState Requested => this.Cache.Get(RequestedId);
        public CallObjectState Accepted => this.Cache.Get(AcceptedId);
        public CallObjectState Withdrawn => this.Cache.Get(WithdrawnId);
        public CallObjectState Rejected => this.Cache.Get(RejectedId);
        public CallObjectState Ended => this.Cache.Get(EndedId);

        private UniquelyIdentifiableCache<CallObjectState> Cache => this.cache ?? (this.cache = new UniquelyIdentifiableCache<CallObjectState>(this.Session));

        protected override void RealTimeSetup(Setup setup)
        {
            new CallObjectStateBuilder(this.Session).WithUniqueId(RequestedId).WithName("Requested").Build();
            new CallObjectStateBuilder(this.Session).WithUniqueId(AcceptedId).WithName("Accepted").Build();
            new CallObjectStateBuilder(this.Session).WithUniqueId(WithdrawnId).WithName("Withdrawn").Build();
            new CallObjectStateBuilder(this.Session).WithUniqueId(RejectedId).WithName("Rejected").Build();
            new CallObjectStateBuilder(this.Session).WithUniqueId(EndedId).WithName("Ended").Build();
        }

        protected override void RealTimeSecure(Security config)
        {
            var full = new[] { Operations.Read, Operations.Write, Operations.Execute };

            config.GrantAdministrator(this.ObjectType, full);

            config.GrantMember(this.ObjectType, Operations.Read);
        }
    }
}
