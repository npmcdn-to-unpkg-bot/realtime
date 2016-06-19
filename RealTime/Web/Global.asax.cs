namespace Web
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Allors;

    public class WebApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var configuration = new Allors.Adapters.Object.SqlClient.Configuration
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                ObjectFactory = Config.ObjectFactory,
            };
            Config.Default = new Allors.Adapters.Object.SqlClient.Database(configuration);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AllorsConfig.Register();
        }
    }
}
