namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("5C26F75F-7179-422B-9864-EE20D34A386E")]
    #endregion
    public partial class Call : AccessControlledObject, Deletable, UniquelyIdentifiable
    {
        #region inherited properties

        public Guid UniqueId { get; set; }

        public Permission[] DeniedPermissions { get; set; }

        public SecurityToken[] SecurityTokens { get; set; }

        #endregion

        #region Allors
        [Id("397115D8-2EA5-4E5F-B0DE-30C45F59CEAB")]
        [AssociationId("4535BCBC-02E6-4644-AA2A-8EB0D2C880B8")]
        [RoleId("78B41C6B-A3B0-4FDB-A865-BB305AF8CD44")]
        [Indexed]
        #endregion
        [Multiplicity(Multiplicity.ManyToOne)]
        [Group("Workspace")]
        public Person Initiator { get; set; }
        
        #region inherited methods
        public void OnBuild() { }

        public void OnPostBuild() { }

        public void OnPreDerive() { }

        public void OnDerive() { }

        public void OnPostDerive() { }

        public void Delete(){}
        #endregion
    }
}