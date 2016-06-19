namespace Allors
{
    using System;
    using System.Configuration;
    using System.IO;

    using NLog;

    public abstract class Command
    {
        protected Logger Logger { get; }

        protected DirectoryInfo DataPath => new DirectoryInfo(ConfigurationManager.AppSettings["dataPath"]);

        //protected FileInfo PopulationFile => new FileInfo(Path.Combine(this.DataPath.FullName, "population.xml"));

        protected FileInfo PopulationFile => new FileInfo("population.xml");

        protected IDatabase SnapshotDatabase
        {
            get
            {
                var configuration = new Adapters.Object.SqlClient.Configuration
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                    ObjectFactory = Config.ObjectFactory,
                    IsolationLevel = System.Data.IsolationLevel.Snapshot,
                    CommandTimeout = 0
                };
                return new Adapters.Object.SqlClient.Database(configuration);
            }
        }

        protected IDatabase RepeatableReadDatabase
        {
            get
            {
                var configuration = new Adapters.Object.SqlClient.Configuration
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                    ObjectFactory = Config.ObjectFactory,
                    IsolationLevel = System.Data.IsolationLevel.RepeatableRead,
                    CommandTimeout = 0
                };
                return new Adapters.Object.SqlClient.Database(configuration);
            }
        }

        protected IDatabase SerializableDatabase
        {
            get
            {
                var configuration = new Adapters.Object.SqlClient.Configuration
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["allors"].ConnectionString,
                    ObjectFactory = Config.ObjectFactory,
                    IsolationLevel = System.Data.IsolationLevel.Serializable,
                    CommandTimeout = 0
                };
                return new Adapters.Object.SqlClient.Database(configuration);
            }
        }

        public abstract void Execute();

        protected Command()
        {
            this.Logger = LogManager.GetCurrentClassLogger();
        }

        private void Derive(ISession session, Extent extent)
        {
            var derivation = new Domain.NonLogging.Derivation(session, extent.ToArray());
            var validation = derivation.Derive();
            if (validation.HasErrors)
            {
                foreach (var error in validation.Errors)
                {
                    this.Logger.Error(error.Message);
                }

                throw new Exception("Derivation Error");
            }
        }
    }
}
