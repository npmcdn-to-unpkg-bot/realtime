namespace Web.Controllers
{
    using System;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.Security;

    using Allors;

    public class TestController : Controller
    {
        private bool IsProduction
        {
            get
            {
                var production = WebConfigurationManager.AppSettings["production"];
                return string.IsNullOrWhiteSpace(production) || bool.Parse(production);
            }
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            if (!this.Request.IsLocal)
            {
                throw new Exception();
            }

            if (this.IsProduction)
            {
                throw new Exception();
            }

            return base.BeginExecuteCore(callback, state);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Init()
        {
            if (this.IsProduction)
            {
                throw new Exception("Init is not supported in production");
            }

            Config.TimeShift = null;
            Config.Default.Init();
            Config.Serializable?.Init();

            return this.View();
        }

        [HttpGet]
        public ActionResult Login(string user, string returnUrl)
        {
            if (this.IsProduction)
            {
                throw new Exception("Programmatic login is not supported in production");
            }

            if (string.IsNullOrWhiteSpace(user))
            {
                user = @"koen@inxin.com";
            }

            FormsAuthentication.SetAuthCookie(user, false);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult TimeShift(int days, int hours = 0, int minutes = 0, int seconds = 0)
        {
            if (this.IsProduction)
            {
                throw new Exception("Time shifting is not supported in production");
            }

            Config.TimeShift = new TimeSpan(days, hours, minutes, seconds);

            return this.RedirectToAction("Index", "Home");
        }
    }
}