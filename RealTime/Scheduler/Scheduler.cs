namespace Allors
{
    using System.Configuration;

    using NLog;

    public abstract class Scheduler
    {
        protected Logger Logger { get; }

        protected string DataPath => ConfigurationManager.AppSettings["dataPath"];

        protected string Link => ConfigurationManager.AppSettings["link"];
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

        public abstract void Schedule();

        protected Scheduler()
        {
            this.Logger = LogManager.GetCurrentClassLogger();
        }
    }
}
