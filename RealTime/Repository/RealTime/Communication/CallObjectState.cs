namespace Allors.Repository.Domain
{
    using System;

    #region Allors
    [Id("973E8C06-332F-4FC2-827B-9BFB194915DA")]
    #endregion
    public partial class CallObjectState : ObjectState 
    {
        #region inherited properties
        public Guid UniqueId { get; set; }

        public Permission[] DeniedPermissions { get; set; }

        public string Name { get; set; }
        #endregion

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
        #endregion
    }
}