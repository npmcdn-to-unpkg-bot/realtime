namespace Allors.Commands
{
    public class Custom : Command
    {
        public override void Execute()
        {
            var meta = Config.Default.MetaPopulation;
            var validationLog = meta.Validate();
        }
    }
}
