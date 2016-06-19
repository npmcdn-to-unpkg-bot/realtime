// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Medias.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    public partial class Medias
    {
        protected override void RealTimeSecure(Security config)
        {
            config.GrantMember(this.ObjectType, Operations.Read);
        }
    }
}
