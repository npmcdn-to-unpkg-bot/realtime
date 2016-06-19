namespace Web.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using Allors;
    using Allors.Domain;
    using Allors.Meta;

    public class GoController : RealTimeController
    {
        private const string CookieName = "GoTo";

        [Authorize]
        [HttpGet]
        public ActionResult To(string id)
        {
            IObject @object = null;

            Guid uniqueId;
            if (Guid.TryParse(id, out uniqueId))
            {
                @object = new UniquelyIdentifiables(this.AllorsSession).FindBy(M.UniquelyIdentifiable.UniqueId, uniqueId);
            }
            else
            {
                long objectId;
                if (long.TryParse(id, out objectId))
                {
                    @object = this.AllorsSession.Instantiate(objectId);
                }
            }

            if (@object != null)
            {
                string url = null;

                if (@object is Person)
                {
                    url = $"profile.mine";
                }

                var cookie = new HttpCookie(CookieName) { Value = url };
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
            else
            {
                this.ControllerContext.HttpContext.Response.Cookies.Remove(CookieName);   
            }
            
            return this.View();
        }
    }
}