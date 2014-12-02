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
	using Library.Resources;
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
				var propertyType = property.GetType();
				var propertyName = propertyType.Name;

				bool isActive = (bool)propertyType.GetProperty("active").GetValue(property, null);
				bool isAdmin = (bool)propertyType.GetProperty("admin").GetValue(property, null);

				if(isActive && (isAdmin && User.IsInRole("Admin") || !isAdmin)){
					string title = Translate.ResourceManager.GetString(propertyName + "Name");

					navigation.Add(new NavigationItem() { 
						Title = title,
						Url = "/" + propertyName + "/List"
					});
				}
			}
            return View(navigation);
        }
    }
}