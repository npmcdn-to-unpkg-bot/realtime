namespace Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web.Database;

    public class MainController : PullController
    {
        [Authorize]
        [HttpPost]
        public ActionResult Pull()
        {
            var response = new PullResponseBuilder(this.AllorsUser);

            var me = (Person)this.AllorsUser;
            response.AddObject("me", me, M.Person.MainTree);

            var onlinePeople = new People(this.AllorsSession).Extent();
            onlinePeople.Filter.AddEquals(M.Person.IsOnline, true);
            response.AddCollection("onlinePeople", onlinePeople, M.Person.MainOnlineTree);

            if (me.EndPoint.ExistCall)
            {
                var call = me.EndPoint.Call;
                var remoteEndPoint = call.EndPointsWhereCall.FirstOrDefault(v => !me.EndPoint.Equals(v));
                response.AddObject("remoteEndPoint", remoteEndPoint, M.EndPoint.MainTree);
            }

            return this.JsonSuccess(response.Build());
        }
    }
}