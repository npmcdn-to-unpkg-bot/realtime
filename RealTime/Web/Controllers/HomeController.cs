namespace Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : RealTimeController
    {
        [Authorize]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}