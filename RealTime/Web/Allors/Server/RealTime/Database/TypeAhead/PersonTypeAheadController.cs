namespace Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web.Database;

    public class PersonTypeAheadController : PullController
    {
        [Authorize]
        [HttpPost]
        public ActionResult Pull(string criteria)
        {
            var response = new PullResponseBuilder(this.AllorsUser);

            var persons = new People(this.AllorsSession).Extent();
            persons.Filter.AddLike(M.Person.UserName.RoleType, criteria + "%");

            response.AddCollection("results", persons.Take(100));

            return this.JsonSuccess(response.Build());
        }
    }
}