namespace Web.Controllers
{
    using System;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.Security;

    using Allors;

    public class LocalController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            if (!this.Request.IsLocal)
            {
                throw new Exception();
            }

            return base.BeginExecuteCore(callback, state);
        }

        public bool IsProduction 
        {
            get
            {
                var production = WebConfigurationManager.AppSettings["production"];
                return string.IsNullOrWhiteSpace(production) || bool.Parse(production);
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }
        
        [HttpPost]
        public ActionResult Setup()
        {
            if (this.IsProduction)
            {
                throw new Exception("Setup is not supported in production, use Console.");
            }

            var database = Config.Default;
            database.Init();

            using (var session = database.CreateSession())
            {
                new Setup(session, null).Apply();

                session.Derive();
                session.Commit();
            }

            return this.View("Index");
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
                user = @"john@doe.com";
            }

            FormsAuthentication.SetAuthCookie(user, false);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}