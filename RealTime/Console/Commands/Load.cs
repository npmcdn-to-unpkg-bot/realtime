namespace Allors.Commands
{
    using System.IO;
    using System.Xml;

    public class Load : Command
    {
        public override void Execute()
        {
            var database = this.RepeatableReadDatabase; // Or Serializable

            using (var reader = new XmlTextReader(this.PopulationFile.FullName))
            {
                this.Logger.Info("Loading from " + this.PopulationFile.FullName);
                database.Load(reader);
                this.Logger.Info("Loaded from " + this.PopulationFile.FullName);
            }
        }
    }
}
