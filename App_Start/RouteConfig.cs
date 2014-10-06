﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Responsive.Filters;
using Responsive.Models;
using System.Web.UI;

namespace Responsive
{
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
        
            // Custom MVC route
            routes.MapRoute(
                name: "Custom",
                url: "{*path}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            ).RouteHandler = new ApplicationRouteHandler();

			// For all @Html.Action boxes
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
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
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            string path = requestContext.RouteData.Values["path"] as string;
                   path = (path == null)? "" : path;

            Navigation page = null;
         
            using(var db = new ResponsiveContext()) {
				page = db.Navigation.FirstOrDefault(x => x.Navigation_Content.FirstOrDefault().Url == path);
            }
           
           if (page == null)
                return new MvcHandler(requestContext);
                //return ParserError(requestContext);

            requestContext.RouteData.Values["controller"] = "Home";//page.Controller;
            requestContext.RouteData.Values["action"] = "About";//page.Action;
            requestContext.RouteData.Values["id"] = page.Navigation_Id;

            return new MvcHandler(requestContext);
        }
    }
}