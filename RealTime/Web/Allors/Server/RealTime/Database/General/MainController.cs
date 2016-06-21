namespace Web.Controllers
{
    using System.Web.Mvc;

    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web.Database;

    public class MainController : PullController
    {
        [Authorize]
        [HttpPost]
        public ActionResult Pull(string existingCallId)
        {
            var response = new PullResponseBuilder(this.AllorsUser);

            var me = (Person)this.AllorsUser;
            response.AddObject("me", me, M.Person.MainTree);

            var existingCall = this.AllorsSession.Instantiate(existingCallId);
            response.AddObject("existingCall", existingCall, M.Call.MainTree);

            return this.JsonSuccess(response.Build());
        }
    }
}