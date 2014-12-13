namespace Cms.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Web.Helpers;
	using System.Web.Mvc;

	using Library.Classes;
	using Library.Resources;

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
			return View(model);
		}

		// GET: Box/Metadata
		public ActionResult Metadata(ArticleItem model)
		{
			return View(model.Metadata.FirstOrDefault());
		}

		// GET: Box/Publishdata
		public ActionResult Publishdata(ArticleItem model)
		{
			List<WebGridColumn> columns = new List<WebGridColumn>();
			columns.Add(new WebGridColumn() { Header = Translate.WebgridPublishedDate, ColumnName = "Published_Date", CanSort = true, Format = item => String.Format("{0:g}",item["Published_Date"]) });
			columns.Add(new WebGridColumn() { Header = Translate.WebgridPublishedBy, ColumnName = "Published_By", CanSort = true });

			ViewBag.Columns = columns;

			return View(model.PublishLogs);
		}

		// GET: Box/Actions
		public ActionResult Actions()
		{
			return View();
		}

    }
}