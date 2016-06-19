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
        public ActionResult Pull()
        {
            var response = new PullResponseBuilder(this.AllorsUser);

            response.AddObject("person", this.AllorsUser, M.Person.MainTree);

            var onlinePeople = new People(this.AllorsSession).Extent();
            onlinePeople.Filter.AddEquals(M.Person.IsOnline, true);
            response.AddCollection("onlinePeople", onlinePeople, M.Person.MainOnlineTree);

            return this.JsonSuccess(response.Build());
        }
    }
}