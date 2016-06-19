namespace Allors.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using Domain;

    public class Upgrade : Command
    {
        private readonly HashSet<Guid> excludedObjectTypes = new HashSet<Guid>
        {
        };

        private readonly HashSet<Guid> excludedRelationTypes = new HashSet<Guid>
        {
        };

        private readonly HashSet<Guid> movedRelationTypes = new HashSet<Guid>
        {
        };

        public override void Execute()
        {
            var database = this.RepeatableReadDatabase; // Or Serializable
            
            try
            {
                this.Logger.Info("Upgrading Allors");

                var notLoadedObjectTypeIds = new HashSet<Guid>();
                var notLoadedRelationTypeIds = new HashSet<Guid>();

                var notLoadedObjects = new HashSet<string>();

                using (var reader = new XmlTextReader(this.PopulationFile.FullName))
                {
                    database.ObjectNotLoaded += (sender, args) =>
                    {
                        if (!this.excludedObjectTypes.Contains(args.ObjectTypeId))
                        {
                            notLoadedObjectTypeIds.Add(args.ObjectTypeId);
                        }
                        else
                        {
                            var id = args.ObjectId.Substring(0, args.ObjectId.IndexOf(":", StringComparison.Ordinal));
                            notLoadedObjects.Add(id);
                        }
                    };

                    database.RelationNotLoaded += (sender, args) =>
                    {
                        if (!this.excludedRelationTypes.Contains(args.RelationTypeId))
                        {
                            if (!notLoadedObjects.Contains(args.AssociationId))
                            {
                                notLoadedRelationTypeIds.Add(args.RelationTypeId);
                            }
                        }
                    };

                    this.Logger.Info("Loading");
                    database.Load(reader);
                    this.Logger.Info("Loaded");
                }

                if (notLoadedObjectTypeIds.Count > 0)
                {
                    var notLoaded = notLoadedObjectTypeIds
                        .Aggregate("Could not load following ObjectTypeIds: ", (current, objectTypeId) => current + ("- " + objectTypeId));

                    this.Logger.Error(notLoaded);
                    return;
                }

                if (notLoadedRelationTypeIds.Count > 0)
                {
                    var notLoaded = notLoadedRelationTypeIds
                        .Aggregate("Could not load following RelationTypeIds: ", (current, relationTypeId) => current + ("- " + relationTypeId));

                    this.Logger.Error(notLoaded);
                    return;
                }

                using (var session = database.CreateSession())
                {
                    new Allors.Upgrade(session).Execute();
                    session.Commit();

                    new Permissions(session).Sync();
                    new Security(session).Apply();

                    session.Commit();
                }

                this.Logger.Info("Upgraded");
            }
            catch
            {
                this.Logger.Info("Please correct errors or restore from backup");
                throw;
            }
        }
    }
}
