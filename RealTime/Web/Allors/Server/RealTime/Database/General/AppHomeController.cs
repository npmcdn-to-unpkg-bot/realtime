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
            
            return this.JsonSuccess(response.Build());
        }
    }
}