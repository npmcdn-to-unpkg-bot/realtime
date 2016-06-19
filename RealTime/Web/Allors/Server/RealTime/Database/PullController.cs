using System;

namespace Web.Controllers
{
    using System.Web.Mvc;

    using Allors;
    using Allors.Web.Database;

    public abstract class PullController : RealTimeController
    {
        protected JsonResult Derive(Action action = null)
        {
            var invokeResponse = new InvokeResponse();

            var validation = this.AllorsSession.Derive();
            if (validation.HasErrors)
            {
                invokeResponse.AddDerivationErrors(validation);
            }
            else
            {
                this.AllorsSession.Commit();
                action?.Invoke();
            }
           
            return this.JsonSuccess(invokeResponse);
        }
    }
}