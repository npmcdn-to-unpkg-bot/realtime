namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("6FC321B9-4B7B-4AB2-9AB0-7CFB568F58FD")]
    #endregion
    public partial class Person : User, Deletable, UniquelyIdentifiable
    {
        #region inherited properties
        public bool UserEmailConfirmed { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string UserPasswordHash { get; set; }

        public SecurityToken OwnerSecurityToken { get; set; }

        public AccessControl OwnerAccessControl { get; set; }

        public Permission[] DeniedPermissions { get; set; }

        public SecurityToken[] SecurityTokens { get; set; }

        public Locale Locale { get; set; }

        public Guid UniqueId { get; set; }

        #endregion
        
        #region Allors
        [Id("4E9331A3-62D4-4D90-A13D-2955C9057D0F")]
        [AssociationId("5B0E5164-BCEF-4346-B1B3-31A709EC2C5D")]
        [RoleId("2687922A-43F2-48AD-83B7-9B1005C9A9E4")]
        #endregion
        [Group("Workspace")]
        public bool IsOnline { get; set; }

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