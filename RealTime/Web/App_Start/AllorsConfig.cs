namespace Web
{
    using System.Web.Configuration;

    using Allors.Meta;

    public class AllorsConfig
    {
        public static bool IsProduction
        {
            get
            {
                var production = WebConfigurationManager.AppSettings["production"];
                return string.IsNullOrWhiteSpace(production) || bool.Parse(production);
            }
        }

        public static void Register()
        {
            // Initialize meta population
            var metaPopulation = MetaPopulation.Instance;

            if (IsProduction)
            {
                // Warm up caches
            }
        }
    }
}
