namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("112D30DB-CE38-42C2-84C8-58BC439FEA32")]
    #endregion
    public partial class EndPoint : AccessControlledObject, Deletable, UniquelyIdentifiable
    {
        #region inherited properties

        public Guid UniqueId { get; set; }

        public Permission[] DeniedPermissions { get; set; }

        public SecurityToken[] SecurityTokens { get; set; }

        #endregion

        #region Allors
        [Id("76301696-762E-4995-A87A-DE6F13366212")]
        [AssociationId("818E6785-FC0F-4385-9CD8-37916996D344")]
        [RoleId("3DFD5DB0-7F00-4BDF-98E5-B6349CC07E45")]
        [Indexed]
        #endregion
        [Group("Workspace")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public Call Call { get; set; }

        #region Allors
        [Id("BF267B7A-2198-4F7C-BDD1-E1314FF4B4E9")]
        [AssociationId("4DAD958E-B609-4C01-8857-44867543A70E")]
        [RoleId("2EB3B531-27BE-4A9C-8A8E-D35D1D0E3AE2")]
        [Indexed]
        #endregion
        [Group("Workspace")]
        [Multiplicity(Multiplicity.OneToMany)]
        public Signal[] Signals { get; set; }

        #region Allors
        [Id("D564E81B-F3FD-40FE-A39A-75BF10314082")]
        [AssociationId("E3FE87CD-0532-4AD9-8474-4B84BDAEEA96")]
        [RoleId("E5DBCD48-FF7A-497E-8A0B-4B6175DE30BE")]
        #endregion
        [Group("Workspace")]
        [Required]
        public bool Established { get; set; }

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