namespace Web.Controllers
{
    using System.Web.Mvc;

    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web.Database;

    public class AppHomeController : PullController
    {
        [Authorize]
        [HttpPost]
        public JsonResult Pull()
        {
            var response = new PullResponseBuilder(this.AllorsUser);

            var me = (Person)this.AllorsUser;
            response.AddObject("me", me, M.Person.AppHomeTree);

            var onlinePeople = new People(this.AllorsSession).Extent();
            onlinePeople.Filter.AddEquals(M.Person.IsOnline, true);
            response.AddCollection("onlinePeople", onlinePeople, M.Person.MainOnlineTree);

            var requested = new CallObjectStates(this.AllorsSession).Requested;

            var calls = new Calls(this.AllorsSession).Extent();
            calls.Filter.AddEquals(M.Call.CurrentObjectState, requested);
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