// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setup.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors
{
    using System;
    using System.IO;
    using System.Reflection;

    using Allors.Domain;
    using Allors.Domain.RealTime.Extensions;

    public partial class Setup
    {

        private void RealTimeOnPrePrepare()
        {
        }

        private void RealTimeOnPostPrepare()
        {
        }

        private void RealTimeOnPreSetup()
        {
        }

        private void RealTimeOnPostSetup()
        {
            var john = new PersonBuilder(this.session)
                .WithUserName("john@doe.com")
                .WithUserEmail("john@doe.com")
                .WithUserEmailConfirmed(true)
                .WithUserPasswordHash("john".HashPassword())
                .Build();

            var jane = new PersonBuilder(this.session)
                .WithUserName("jane@doe.com")
                .WithUserEmail("jane@doe.com")
                .WithUserEmailConfirmed(true)
                .WithUserPasswordHash("jane".HashPassword())
                .Build();
        }

        private Media LoadMedia(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        var inData = ms.ToArray();
                        var media = new MediaBuilder(this.session).WithInData(inData).Build();
                        return media;
                    }
                }
                else
                {
                    throw new Exception("Resource " + resourceName + " not found.");
                }
            }
        }
    }
}