namespace Web.Hubs
{
    using System.Threading.Tasks;

    using Allors;
    using Allors.Domain;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    public interface IAllorsHub
    {
        void onRefresh();

        void onReady(string callId);

        void onCandidate(string callId, string candidate);

        void onOffer(string callId, string offer);

        void onAnswer(string callId, string offer);
    }

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

                return Task.Factory.StartNew(this.Refresh);
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

                    return Task.Factory.StartNew(this.Refresh);
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

                    return Task.Factory.StartNew(this.Refresh);
                }
            }

            return base.OnReconnected();
        }

        public void Refresh()
        {
            this.Clients.Others.onRefresh();
        }

        public void Refresh(string userName)
        {
            this.Clients.User(userName).onRefresh();
        }

        public void Ready(string userName, string callId)
        {
            this.Clients.User(userName).onReady(callId);
        }

        public void Candidate(string userName, string callId, string candidate)
        {
            this.Clients.User(userName).onCandidate(callId, candidate);
        }

        public void Offer(string userName, string callId, string offer)
        {
            this.Clients.User(userName).onOffer(callId, offer);
        }

        public void Answer(string userName, string callId, string answer)
        {
            this.Clients.User(userName).onAnswer(callId, answer);
        }

        private static ISession CreateSession()
        {
            return Config.Default.CreateSession();
        }
    }
}
