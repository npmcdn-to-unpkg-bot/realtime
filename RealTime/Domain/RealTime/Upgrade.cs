namespace Allors
{
    using System;

    using Allors.Domain.NonLogging;

    using Domain;
    using NLog;

    public class Upgrade
    {
        private readonly ISession session;

        private readonly Logger logger;

        public Upgrade(ISession session)
        {
            this.session = session;
            this.logger = LogManager.GetCurrentClassLogger();
        }

        public void Execute()
        {
            this.logger.Info("Update People");
            var objects = new People(this.session).Extent();

            var derivation = new Derivation(this.session, objects);
            var derivationLog = derivation.Derive();

            if (derivationLog.HasErrors)
            {
                this.session.Rollback();
            }
            else
            {
                this.session.Commit();
            }
        }

        private void Derive(Extent extent)
        {
            var derivation = new Domain.NonLogging.Derivation(this.session, extent.ToArray());
            var validation = derivation.Derive();
            if (validation.HasErrors)
            {
                foreach (var error in validation.Errors)
                {
                    Console.WriteLine(error.Message);
                }

                throw new Exception("Derivation Error");
            }
        }
    }
}