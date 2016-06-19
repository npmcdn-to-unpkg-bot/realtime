namespace Allors.Commands
{
    using System.Text;
    using System.Xml;

    public class Save : Command
    {
        public override void Execute()
        {
            var database = this.SnapshotDatabase;

            using (var writer = new XmlTextWriter(this.PopulationFile.FullName, Encoding.UTF8))
            {
                this.Logger.Info("Saving to " + this.PopulationFile.FullName);
                database.Save(writer);
                this.Logger.Info("Saved to " + this.PopulationFile.FullName);
            }
        }
    }
}
