namespace Web.Controllers
{
    using System.Web.Mvc;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Transports;

    using Web.Hubs;

    public class ConnectionsController : RealTimeController
    {
        [HttpPost]
        public ActionResult VerifyConnections()
        {

            var heartBeat = GlobalHost.DependencyResolver.Resolve<ITransportHeartbeat>();
            var context = GlobalHost.ConnectionManager.GetHubContext<AllorsHub>();

            //TODO: Verify Connections

            return new EmptyResult();
        }
    }
}