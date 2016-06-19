// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionExtensions.cs" company="LngLng bvba">
//   Copyright 2016 LngLng bvba.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain.RealTime.Extensions
{
    using System;
    using System.Net;

    public static class SessionExtensions
    {
        public static Media SetupImage(this ISession session, Uri uri)
        {
            try
            {
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(uri.AbsoluteUri);
                var media = new MediaBuilder(session).WithInData(imageBytes).Build();
                return media;
            }
            catch
            {
                return null;
            }
        }

        public static Media SetupImageFromUrl(this ISession session, string url)
        {
            try
            {
                var webClient = new WebClient();
                var imageBytes = webClient.DownloadData(url);
                var media = new MediaBuilder(session).WithInData(imageBytes).Build();
                return media;
            }
            catch
            {
                return null;
            }
        }
    }
}
