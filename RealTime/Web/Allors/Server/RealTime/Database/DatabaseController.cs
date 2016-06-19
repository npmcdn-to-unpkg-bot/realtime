namespace Web.Controllers
{
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web.Database;

    public class DatabaseController : RealTimeController
    {
        private const string Group = Groups.Workspace;
        
        [Authorize]
        [HttpPost]
        public ActionResult Sync(SyncRequest syncRequest)
        {
            var user = this.AllorsUser ?? Singleton.Instance(this.AllorsSession).Guest;
            var response = new SyncResponseBuilder(this.AllorsSession, user, syncRequest, Group);
            return this.JsonSuccess(response.Build());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Push(PushRequest pushRequest)
        {
            var user = this.AllorsUser ?? Singleton.Instance(this.AllorsSession).Guest;
            var response = new PushResponseBuilder(this.AllorsSession, user, pushRequest, Group);
            return this.JsonSuccess(response.Build());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Invoke(InvokeRequest invokeRequest)
        {
            var user = this.AllorsUser ?? Singleton.Instance(this.AllorsSession).Guest;
            var response = new InvokeResponseBuilder(this.AllorsSession, user, invokeRequest, Group);
            return this.JsonSuccess(response.Build());
        }

        [HttpGet]
        public ActionResult Translate(string lang)
        {
            var cultureInfo = new CultureInfo(lang);

            var workspaceMetaResources = Resources.WorkspaceMeta.ResourceManager.GetResourceSet(cultureInfo, true, true)
                .Cast<DictionaryEntry>()
                .ToDictionary(entry => "meta_" + entry.Key.ToString().Replace(".", "_"), entry => entry.Value.ToString());


            var workspaceResources = Resources.WorkspaceResources.ResourceManager.GetResourceSet(cultureInfo, true, true)
                .Cast<DictionaryEntry>()
                .ToDictionary(entry => entry.Key.ToString(), entry => entry.Value.ToString());
                
            var resources = workspaceResources
                .Concat(workspaceMetaResources)
                .ToDictionary(x => x.Key, x => x.Value);

            return this.JsonSuccess(resources);
        }
    }
}