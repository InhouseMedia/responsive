namespace Cms.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Dynamic;
	using System.Reflection;
	using System.Resources;
	using System.Linq;
	using System.Web.Helpers;
	using System.Web.Mvc;

	using Library.Classes;
	using Library.Resources;
	using Library.Models;

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

		// GET: Box/General
		public ActionResult General(ArticleItem model)
		{
			// ConfigClass for all visibility options
			var se = ConfigClass.Settings.searchEngines;
			Type t = typeof(ConfigClass.visibility);
			var properties = t.GetProperties();

			Dictionary<string, BoxVisibility> items = new Dictionary<string, BoxVisibility>();

			// Get all translations so that we can get the ones for the visibility buttons
			var manager = new ResourceManager(typeof(Translate));

			foreach (var item in properties)
			{
				// Get current visibility value from the ConfigClass
				int x = (int)item.GetValue(se.visibility);

				BoxVisibility boxObject = new BoxVisibility();
				boxObject.Active = (x == model.Active) ? "active" : "";
				boxObject.Name = item.Name;
				boxObject.Key = x;
				boxObject.Translation = manager.GetString(item.Name);

				items.Add(manager.GetString(item.Name),boxObject);
			}
			
			// Define the model with multiple object
			dynamic mymodel = new ExpandoObject();
			mymodel.Visibility = items;
			mymodel.Data = model;

			return View(mymodel);
		}

		// GET: Box/Metadata
		public ActionResult Metadata(ArticleItem model)
		{
			return View(model.Metadata);
		}

		// GET: Box/Publishdata
		public ActionResult Publishdata(ArticleItem model)
		{
			List<WebGridColumn> columns = new List<WebGridColumn>();
			columns.Add(new WebGridColumn() { Header = Translate.WebgridPublishedDate, ColumnName = "Published_Date", CanSort = true, Format = item => String.Format("{0:g}",item["Published_Date"]) });
			columns.Add(new WebGridColumn() { Header = Translate.WebgridPublishedBy, ColumnName = "AspNetUsers.UserName", CanSort = true });

			ViewBag.Columns = columns;
	
			return View(model.PublishLogs);
		}

		// GET: Box/Actions
		public ActionResult Actions()
		{
			string[] articleActions = ConfigClass.Settings.controllers.article.actions;
			
			List<WebGridColumn> columns = new List<WebGridColumn>();
			columns.Add(new WebGridColumn() { Header = Translate.WebgridType, ColumnName = "ActionName", Format = item => item, CanSort = true });

			return View(articleActions);
		}

    }
}