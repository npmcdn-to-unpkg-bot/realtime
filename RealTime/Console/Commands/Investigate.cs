namespace Allors.Commands
{
    using System;
    using System.Linq;

    using Allors.Domain;
    using Allors.Meta;

    public class Investigate : Command
    {
        private readonly IDatabase database;

        public Investigate()
        {
            this.database = this.SnapshotDatabase;
        }

        public override void Execute()
        {
            this.SecurityReport();
        }

        public void SecurityReport()
        {
            using (var session = database.CreateSession())
            {
                Console.WriteLine("All Roles:");
                foreach (Role specificRole in new Roles(session).Extent())
                {
                    Console.WriteLine("- " + specificRole.Name);
                }

                Console.WriteLine("Press any key to continue...\n");
                Console.ReadKey();

                Console.WriteLine("Security tokens for Administrator:");
                var admin = new People(session).FindByUsername("Adminsitrator");

                foreach (SecurityToken token in admin.SecurityTokens)
                {
                    Console.WriteLine("- " + token);
                }
            }
        }

        public void IndexReport()
        {
            var metaPopulation = this.database.MetaPopulation;
            var relationTypes = metaPopulation.RelationTypes.Where(v => v.RoleType.ObjectType.IsComposite && v.IsIndexed == false).ToList();
            relationTypes.ForEach(Console.WriteLine);
        }
    }
}
