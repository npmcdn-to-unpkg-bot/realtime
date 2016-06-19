namespace Web.Controllers
{
    using System.Web.Mvc;

    using Allors.Meta;
    using Allors.Web.Database;

    public class MainController : PullController
    {
        [Authorize]
        [HttpPost]
        public ActionResult Pull()
        {
            var response = new PullResponseBuilder(this.AllorsUser);

            response.AddObject("me", this.AllorsUser, M.Person.MainTree);

            return this.JsonSuccess(response.Build());
        }
    }
}