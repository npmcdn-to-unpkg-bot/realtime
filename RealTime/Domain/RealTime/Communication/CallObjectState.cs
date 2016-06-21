// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Communication.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    public partial class CallObjectState
    {
        public bool IsRequested => this.UniqueId == CallObjectStates.RequestedId;

        public bool IsAccepted => this.UniqueId == CallObjectStates.AcceptedId;

        public bool IsWithdrawn => this.UniqueId == CallObjectStates.WithdrawnId;

        public bool IsRejected => this.UniqueId == CallObjectStates.RejectedId;

        public bool IsEnded => this.UniqueId == CallObjectStates.EndedId;

        public override string ToString()
        {
            return this.Name;
        }
    }
}
