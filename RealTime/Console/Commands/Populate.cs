namespace Allors.Commands
{
    using System;
    using System.Globalization;

    public class Populate : Command
    {
        public override void Execute()
        {
            var database = this.RepeatableReadDatabase; // Or Serializable

            Console.WriteLine("Are you sure, all current data will be destroyed? (Y/N)\n");

            this.Logger.Info("Allors");
            var confirmationKey = Console.ReadKey(true).KeyChar.ToString(CultureInfo.InvariantCulture);
            if (confirmationKey.ToLower().Equals("y"))
            {
                database.Init();

                using (var session = database.CreateSession())
                {
                    new Setup(session, null).Apply();

                    // new Allors.Upgrade(session).Execute();
                    
                    var validation = session.Derive();
                    if (validation.HasErrors)
                    {
                        foreach (var error in validation.Errors)
                        {
                            this.Logger.Error(error);
                        }

                        this.Logger.Info("Not populated");
                    }
                    else
                    {
                        session.Commit();

                        this.Logger.Info("Populated");
                    }
                }
            }
        }
    }
}
