using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Library.Classes;
using Library.Setup;

namespace Cms
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			//--Generate standard database (if no data exsists)
			AutoFillDatabaseClass.GetScripts();
			//--Merge standard config with user specific config
			ConfigClass.setConfig();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
