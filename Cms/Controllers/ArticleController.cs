namespace Cms.Controllers
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Microsoft.Owin.Security;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Web.Helpers;
	using System.Web.Mvc;

	using Library.Classes;
	using Library.Models;

	[Authorize]
	public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

		// GET: Article/List
		public ActionResult List()
		{
			var currentAdminRole = User.IsInRole("Admin");
			var currentManagerRole = User.IsInRole("Admin");

			List<ArticleListItem> articleList = ArticleClass.getArticleList();

			List<WebGridColumn> columns = new List<WebGridColumn>();
			//columns.Add(new WebGridColumn() { ColumnName = "Id", Header = "Id", CanSort = true });
			columns.Add(new WebGridColumn() { ColumnName = "Title", Header = "Title", CanSort = true });
			columns.Add(new WebGridColumn() { ColumnName = "CreationDate", Header = "Created on", CanSort = true });
			columns.Add(new WebGridColumn() { ColumnName = "ChangedDate", Header = "Changed on", CanSort = true });
			columns.Add(new WebGridColumn() { ColumnName = "PublishedDate", Header = "Published on", CanSort = true });
			columns.Add(new WebGridColumn() { ColumnName = "Active", Header = "Active", CanSort = true, Style = "text-center", Format = (item) => { return new HtmlString("<i class='" + (item["Active"] > 0 ? "glyphicon glyphicon-ok" : "") + "'></i>"); } });
			columns.Add(new WebGridColumn() { ColumnName = "HasKeyWords", Header = "Keywords", CanSort = true, Style = "text-center", Format = (item) => { return new HtmlString("<i class='" + (item["HasKeyWords"] ? "glyphicon glyphicon-ok" : "") + "'></i>"); } });
			columns.Add(new WebGridColumn() { ColumnName = "HasMetaData", Header = "Metadata", CanSort = true, Style = "text-center", Format = (item) => { return new HtmlString("<i class='" + (item["HasMetaData"] ? "glyphicon glyphicon-ok" : "") + "'></i>"); } });

			columns.Add(new WebGridColumn()
			{
				ColumnName = "Change",
				Header = "",
				Style = "text-center",
				Format = (item) =>
				{
					//Managers should not be able to add or remove Admin rights (or remove their own account)
					//bool disableCheckBox = ((!currentAdminRole && !currentManagerRole) || (!currentAdminRole && item["Admin"] == true) || item.Id == User.Identity.GetUserId());
					bool disableCheckBox = false;
					return new HtmlString("<a href='/Article/Change/" + item.Id + "' class='btn btn-default btn-xs" + (disableCheckBox ? " disabled" : "") + "'><i class='glyphicon glyphicon-edit'></i></a>");
				}
			});

			columns.Add(new WebGridColumn()
			{
				ColumnName = "Delete",
				Header = "",
				Style = "text-center",
				Format = (item) =>
				{
					//Managers should not be able to add or remove Admin rights (or remove their own account)
					//bool disableCheckBox = ((!currentAdminRole && !currentManagerRole) || (!currentAdminRole && item["Admin"] == true) || item.Id == User.Identity.GetUserId());
					bool disableCheckBox = false;
					return new HtmlString("<a href='/Article/Delete/" + item.Id + "' class='btn btn-default btn-xs" + (disableCheckBox ? " disabled" : "") + "'><i class='glyphicon glyphicon-trash'></i></a>");
				}
			});


			ViewBag.Columns = columns;

			return View(articleList);
		}

    }
}