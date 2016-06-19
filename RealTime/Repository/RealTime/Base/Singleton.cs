namespace Allors.Repository.Domain
{
    public partial class Singleton
    {
        #region inherited properties
        #endregion

        #region Allors
        [Id("9AB21DB4-E344-4065-A778-64E153D6EF83")]
        [AssociationId("DB5D6EB3-D1DD-42C9-A07E-559590BF9F1A")]
        [RoleId("CD73199F-38D4-4BFF-8F90-EF0496A1DE54")]
        #endregion
        [Multiplicity(Multiplicity.OneToOne)]
        [Indexed]
        public AccessControl DefaultMembersAccessControl { get; set; }
        
        #region inherited methods
        #endregion
    }
}