// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectsBase.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors
{
    using Allors.Domain;

    public abstract partial class ObjectsBase<T>
    {
        protected virtual void RealTimePrepare(Setup setup)
        {
        }

        protected virtual void RealTimeSetup(Setup setup)
        {
        }

        protected virtual void RealTimeSecure(Security config)
        {
        }
    }
}
