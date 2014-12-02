using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Reflection;
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

		protected void Application_AcquireRequestState(object sender, EventArgs e)
		{
			string configLanguage = ConfigClass.Settings.language.locale.First();
			CultureInfo cultureInfo = new CultureInfo(configLanguage);

			/*
			var handler = Context.Handler as MvcHandler;
			var routeData = handler != null ? handler.RequestContext.RouteData : null;
			var routeCulture = routeData != null ? routeData.Values["culture"].ToString() : null;
			var languageCookie = HttpContext.Current.Request.Cookies["lang"];
			//var userLanguages = HttpContext.Current.Request.UserLanguages;
			

			// Set the Culture based on a route, a cookie or the browser settings,
			// or default value if something went wrong
			CultureInfo cultureInfo = new CultureInfo(
			routeCulture ?? (languageCookie != null
				? languageCookie.Value
				: configLanguage != null
					? configLanguage
					: "en-US")
			);
			*/
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
			Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
		}
    }


}
