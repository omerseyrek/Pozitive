using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Core
{

    public class ModuleAuthorizeAttribute : Attribute
    {
        public string Module;

        public ModuleAuthorizeAttribute()
        {
            this.Module = System.Web.HttpContext.Current.
                            Request.RequestContext.RouteData.GetRequiredString("controller");
        }

        public ModuleAuthorizeAttribute(string module)
        {
            this.Module = module;
        }
    }
}
