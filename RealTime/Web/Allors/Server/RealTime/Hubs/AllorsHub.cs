namespace Web.Hubs
{
    using System.Threading.Tasks;

    using Allors;
    using Allors.Domain;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    [HubName("allors")]
    public class AllorsHub : Hub<IAllorsHub>
    {
        public override Task OnConnected()
        {
            var username = this.Context.User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {
                using (var session = CreateSession())
                {
                    var person = new People(session).FindByUsername(username);

                    person.IsOnline = true;

                    session.Derive(true);
                    session.Commit();
                }

                return Task.Factory.StartNew(this.ServerRefresh);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var username = this.Context.User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {
                using (var session = CreateSession())
                {
                    var person = new People(session).FindByUsername(username);

                    person.IsOnline = false;

                    session.Derive(true);
                    session.Commit();

                    return Task.Factory.StartNew(this.ServerRefresh);
                }
            }

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var username = this.Context.User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {
                using (var session = CreateSession())
                {
                    var person = new People(session).FindByUsername(username);

                    person.IsOnline = true;

                    session.Derive(true);
                    session.Commit();

                    return Task.Factory.StartNew(this.ServerRefresh);
                }
            }

            return base.OnReconnected();
        }
        
        public void ServerRefresh()
        {
            this.Clients.Others.clientRefresh();
        }
        
        private static ISession CreateSession()
        {
            return Config.Default.CreateSession();
        }
    }
}
