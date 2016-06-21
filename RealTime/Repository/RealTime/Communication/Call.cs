namespace Allors.Repository.Domain
{
    using System;

    #region Allors

    [Id("80d9e68a-b14f-4a24-ae76-896d7d6acc69")]

    #endregion

    public partial class Call : UniquelyIdentifiable, Transitional, Deletable
    {
        #region inherited properties
        public ObjectState PreviousObjectState { get; set; }

        public ObjectState LastObjectState { get; set; }

        public Permission[] DeniedPermissions { get; set; }

        public SecurityToken[] SecurityTokens { get; set; }

        public Guid UniqueId { get; set; }
        #endregion

        #region Allors
        [Id("081b3085-4227-484a-8889-95162409155e")]
        [AssociationId("514c2c91-3ce8-4842-bf09-05ed5ffeef05")]
        [RoleId("10bd51fe-f210-4202-a247-7251ab5ac579")]
        [Indexed]
        #endregion
        [Group("Workspace")]
        [Multiplicity(Multiplicity.ManyToOne)]
        public CallObjectState CurrentObjectState { get; set; }
       
        #region Allors
        [Id("23D39D0B-4D16-4DA4-B51D-DD7540A95010")]
        [AssociationId("C87987BD-8374-48BB-B37F-188C7FE229C0")]
        [RoleId("C39F9D90-8BAD-487F-91AA-3306379F5EB2")]
        #endregion
        [Group("Workspace")]
        [Derived]
        public DateTime CreationDate { get; set; }

        #region Allors
        [Id("c40f49a0-e9a3-4dae-b86d-5e959e2d9995")]
        [AssociationId("de5adc2b-cd98-45b2-bd5f-849f942e0969")]
        [RoleId("3227b2b2-e199-440c-accb-017d63f522d8")]
        #endregion
        [Group("Workspace")]
        [Derived]
        public DateTime StartDate { get; set; }

        #region Allors
        [Id("eaf5eca6-1249-442f-8df4-8f1b4e662d9e")]
        [AssociationId("dc62c6a8-b9fc-4c56-9918-4918e3074bd2")]
        [RoleId("cad6cee8-fd81-47dd-9bab-fe12e136c0cb")]
        #endregion
        [Group("Workspace")]
        [Derived]
        public DateTime EndDate { get; set; }

        #region Allors
        [Id("46174C09-E117-4A92-AE20-26BA3E24F003")]
        [AssociationId("AEA97334-866F-404E-9659-D784A612A2BC")]
        [RoleId("A6ADE47C-9CF9-4ACE-821A-E05EA4A42345")]
        [Indexed]
        #endregion
        [Multiplicity(Multiplicity.ManyToOne)]
        [Group("Workspace")]
        [Required]
        public Person Caller { get; set; }

        #region Allors
        [Id("BFE0CE54-E890-4388-A41F-34EFD9389AC7")]
        [AssociationId("07B8B10E-47A1-4BCD-A922-4DB7CF9F8EAA")]
        [RoleId("306214AA-3E41-47A4-B6BC-24B5A57BF93E")]
        [Indexed]
        #endregion
        [Multiplicity(Multiplicity.ManyToOne)]
        [Group("Workspace")]
        [Required]
        public Person Callee { get; set; }

        #region Allors
        [Id("D58AC3E1-9B00-4A81-A4C6-7D3BA460A663")]
        #endregion
        [Group("Workspace")]
        public void Accept()
        {
        }

        #region Allors
        [Id("55723E5B-B5B7-4A36-8E1C-6EF4F96275E9")]
        #endregion
        [Group("Workspace")]
        public void Withdraw()
        {
        }

        #region Allors
        [Id("F71760CE-7BBF-46B4-9331-FCD73D5C8CB9")]
        #endregion
        [Group("Workspace")]
        public void Reject()
        {
        }

        #region Allors
        [Id("9765017B-33D9-4283-B63D-6BCA523E8EF0")]
        #endregion
        [Group("Workspace")]
        public void End()
        {
        }
        
        #region inherited methods

        public void OnBuild()
        {
        }

        public void OnPostBuild()
        {
        }

        public void OnPreDerive()
        {
        }

        public void OnDerive()
        {
        }

        public void OnPostDerive()
        {
        }


        public void Delete()
        {
        }

        #endregion
    }
}