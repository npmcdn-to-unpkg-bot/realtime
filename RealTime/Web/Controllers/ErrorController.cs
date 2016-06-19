namespace Web.Error
{
    using System.Web.Mvc;

    public class ErrorController : RealTimeController
    {
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Index()
        {
            return View();
        }
    }
}