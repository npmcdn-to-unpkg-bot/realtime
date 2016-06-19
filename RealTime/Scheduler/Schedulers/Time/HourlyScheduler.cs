using System;
using System.IO;
using System.Net;

namespace Allors
{
    using Allors.Domain;

    public class HourlyScheduler : Scheduler
    {
        public override void Schedule()
        {
            this.VerifyConnections();
        }

        private void VerifyConnections()
        {
            //TODO implement logic here to validate and clean lost connections....
            this.Logger.Info("Verify Connections");

            var url = System.Configuration.ConfigurationManager.AppSettings["webUrl"];

            var request = WebRequest.Create($"{url}Connections/VerifyConnections");

            request.Method = "POST";
            request.ContentLength = 0;

            using (var response = request.GetResponse())
            {
                this.Logger.Info(((HttpWebResponse)response).StatusDescription);
            }
        }
    }
}
