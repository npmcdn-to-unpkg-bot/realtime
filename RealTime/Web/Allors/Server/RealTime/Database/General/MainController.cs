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

            var calls = new Calls(this.AllorsSession).Extent();
            var or = calls.Filter.AddOr();
            or.AddEquals(M.Call.Caller, me);
            or.AddEquals(M.Call.Callee, me);
            response.AddCollection("calls", calls, M.Call.MainTree);

            var callObjectStates = new CallObjectStates(this.AllorsSession).Extent();
            response.AddCollection("callObjectStates", callObjectStates);

            return this.JsonSuccess(response.Build());
        }
    }
}