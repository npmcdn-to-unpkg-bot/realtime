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

        #region Allors
        [Id("6DEF4F8D-4039-4990-8BC7-F6517630194B")]
        [AssociationId("71AD593D-8C04-4D9D-A056-0161FD0B69A7")]
        [RoleId("C3B386A8-A4F8-4544-8EB8-2D8FBC2DF138")]
        [Indexed]
        #endregion
        [Multiplicity(Multiplicity.ManyToMany)]
        [Group("Workspace")]
        [Derived]
        public Call[] RequestedCalls { get; set; }

        #region Allors
        [Id("4655E9AD-B6FA-473A-AA50-E9BF06BDC4BB")]
        [AssociationId("C98E3EEC-D228-442F-B72F-002492EB13F1")]
        [RoleId("A8E542C2-259A-4C55-A2D2-F75B1C9E356E")]
        [Indexed]
        #endregion
        [Multiplicity(Multiplicity.ManyToOne)]
        [Group("Workspace")]
        [Derived]
        public Call[] AcceptedCall { get; set; }

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