namespace Cms.Controllers
{
	using System.IO;
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Web;
	using System.Web.Hosting;
	using System.Web.Mvc;

	using Library.Classes;
	using Library.Setup;
	 
	public class BoxController : Controller
    {
		List<NavigationItem> navigation = new List<NavigationItem>();

        // GET: Box/Navigation
        public ActionResult Navigation()
        {
			ConfigClass.Controllers config = ConfigClass.Settings.controllers;
			PropertyInfo[] controllers = config.GetType().GetProperties();

			foreach (PropertyInfo control in controllers)
			{
				var property = control.GetValue(config, null);
				/*
				foreach (PropertyInfo property in control.GetType().GetProperties()) { 
					if(property.Name == "Active" &&)
				
				}*/

				bool isActive = (bool)property.GetType().GetProperty("active").GetValue(property, null);
				bool isAdmin = (bool)property.GetType().GetProperty("admin").GetValue(property, null);

				if(isActive && (isAdmin && User.IsInRole("Admin") || !isAdmin)){
					navigation.Add(new NavigationItem() { 
						Title = property.GetType().Name,
						Url = "/" + property.GetType().Name + "/List"
					});
				}
			}
            return View(navigation);
        }
    }
}