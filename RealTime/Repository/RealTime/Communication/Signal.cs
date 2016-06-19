namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("07CDFD90-3880-415D-B3C6-A162C1399CB5")]
    #endregion
    public partial class Signal : AccessControlledObject, Deletable, UniquelyIdentifiable
    {
        #region inherited properties

        public Guid UniqueId { get; set; }

        public Permission[] DeniedPermissions { get; set; }

        public SecurityToken[] SecurityTokens { get; set; }

        #endregion

        #region Allors
        [Id("505F5E34-73A7-4AE2-A0F1-74E2248DC618")]
        [AssociationId("0AA84C9B-A0A6-4A21-93B0-C81CDFAAC682")]
        [RoleId("F4FE2F9B-EAED-4C72-B837-56ADD356FB58")]
        #endregion
        [Group("Workspace")]
        [Size(-1)]
        public string Value { get; set; }

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