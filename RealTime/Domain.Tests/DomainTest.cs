namespace Domain
{
    using Allors;
    using Allors.Adapters.Memory;

    using NUnit.Framework;

    using Configuration = Allors.Adapters.Memory.Configuration;

    public class DomainTest
    {
        /// <summary>
        /// Gets the database session.
        /// </summary>
        public ISession Session { get; private set; }

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            this.Setup(true);
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            this.Session.Rollback();
            this.Session = null;
        }

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="populate">
        /// The setup.
        /// </param>
        protected void Setup(bool populate)
        {
            var configuration = new Configuration { ObjectFactory = Config.ObjectFactory };
            Config.Default = new Database(configuration);
            
            var database = Config.Default;
            database.Init();

            this.Session = Config.Default.CreateSession();

            if (populate)
            {
                new Setup(this.Session, null).Apply();
                this.Session.Commit();
            }
        }
    }
}