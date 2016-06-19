namespace Web.Hubs
{

    using Allors.Domain;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Transports;

    public static class HubExtensions
    {
        public static void ResetInvalidConnections(this Person person)
        {
            var heartBeat = GlobalHost.DependencyResolver.Resolve<ITransportHeartbeat>();
            // TODO:
        }
    }
}