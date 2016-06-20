namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("E4DA2DCC-F64F-4911-A7A4-440743EFB5E2")]
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
        [Id("D22C19BB-305A-4A63-88FD-5BEA21D13C65")]
        [AssociationId("CCDF00FF-3590-4A56-9EB2-7311AA6A8079")]
        [RoleId("CF4A7972-5CDF-452D-871C-506B05A87CA9")]
        [Indexed]
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