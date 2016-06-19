namespace Allors.Commands
{
    using System.Configuration;
    using System.Data;

    using Allors.Adapters.Object.SqlClient;
    using Allors.Adapters.Object.SqlClient.Caching.Debugging;
    using Allors.Adapters.Object.SqlClient.Debug;
    using Allors.Domain;
    using Allors.Domain.Logging;

    using Command = Allors.Command;
    using Configuration = Allors.Adapters.Object.SqlClient.Configuration;
    using Object = Allors.Domain.Object;

    public class Performance : Command
    {
        private readonly DebugConnectionFactory connectionFactory;

        private readonly DebugCacheFactory cacheFactory;

        private readonly Database database;

        public Performance()
        {
            this.connectionFactory = new DebugConnectionFactory();
            this.cacheFactory = new DebugCacheFactory();

            var configuration = new Configuration
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                ObjectFactory = Config.ObjectFactory,
                IsolationLevel = IsolationLevel.Snapshot,
                CommandTimeout = 0,
                ConnectionFactory = this.connectionFactory,
                CacheFactory = this.cacheFactory
            };

            this.database = new Database(configuration);
        }

        public override void Execute()
        {
           this.MatchingDerivation();
        }

        private void MatchingDerivation()
        {
            using (var session = this.database.CreateSession())
            {
                // TODO: Trigger a derivation

                var derivationLog = new DerivationLog();
                var derivation = new Derivation(session, derivationLog);
                var validation = derivation.Derive();

                var list = derivationLog.List;
                //derivationLog.List.RemoveAll(v => !v.StartsWith("Dependency"));
            }
        }

        private class DerivationLog : ListDerivationLog
        {
            public override void AddedDerivable(Object derivable)
            {
                base.AddedDerivable(derivable);

                if (derivable.ToString().Equals("BlahBLah"))
                {
                    // Break when "BlahBlah"
                }
            }

            /// <summary>
            /// The dependee is derived before the dependent object;
            /// </summary>
            public override void AddedDependency(Object dependent, Object dependee)
            {
                base.AddedDependency(dependent, dependee);

                if (dependent is Person)
                {
                    // Break when Person
                }

            }
        }
    }
}
