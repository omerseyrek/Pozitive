using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Core
{

    public static class ApplicationConstants
    {
        public static string SuperUser { get { return "admin"; } }
        public static string ModuleCachePrefix { get { return "moduleInRoles_"; } }
        public static string MenuCachePrefix { get { return "UserMenus_"; } }
    }
}
