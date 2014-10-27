namespace Responsive
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;
	using System.Web.UI;

	using Responsive.Filters;
	using Library.Classes;
	using Library.Models;
	using Library.Helpers;

	public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("*.js|css|swf");
            routes.RouteExistingFiles = true;

            routes.MapRoute(
               name: "Sitemap",
               url: "sitemap.xml",
               defaults: new { controller = "Navigation", action = "Sitemap", id = UrlParameter.Optional }
           );

			routes.MapRoute(
			   name: "Generate database",
			   url: "Article/Generate",
			   defaults: new { controller = "Article", action = "Generate", id = UrlParameter.Optional }
		   );

            // Custom MVC route
            routes.MapRoute(
                name: "Custom",
                url: "{*path}",
                defaults: new { controller = "Article", action = "Index", id = UrlParameter.Optional }
            ).RouteHandler = new ApplicationRouteHandler();

			// For all @Html.Action boxes
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Article", action = "Index", id = UrlParameter.Optional }
			);

			/*
			routes.MapRoute(
				name: "Custom",
				url: "{lang}/{*path}",
				defaults: new { controller = "Default", action = "Index" },
				constraints: new { lang = @"fr|en" }
			).RouteHandler = new ApplicationRouteHandler();
			*/
		}
    }

    public class ApplicationRouteHandler : IRouteHandler
    {
        /// <summary>
        /// Provides the object that processes the request.
        /// </summary>
        /// <param name="requestContext">An object that encapsulates information about the request.</param>
        /// <returns>
        /// An object that processes the request.
        /// </returns>
		private string previousPath { get; set; }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
			NavigationItem page = null;
			string path = requestContext.RouteData.Values["path"] as string;
                   path = (path == null)? "/" : "/" + path;

			// Check if page is already visited (F5)
			bool caching = (path == previousPath);		
			previousPath = path;
			
			// Generate navigation List
			NavigationClass.getNavigation(caching);

			page = NavigationClass.urlNavigationItems.FirstOrDefault(x => x.Url == path);
			
			if (NavigationClass.currentNavigationItem == null)
                return new MvcHandler(requestContext);
                //return ParserError(requestContext);

            requestContext.RouteData.Values["controller"] = "Article";//page.Controller;
            requestContext.RouteData.Values["action"] = "Index";//page.Action;
			requestContext.RouteData.Values["id"] = "";//page.ArticleId;

            return new MvcHandler(requestContext);
        }
    }
}