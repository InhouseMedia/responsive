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
	using Library.Resources;

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
			columns.Add(new WebGridColumn() { Header = Translate.WebgridTitle, ColumnName = "Title", CanSort = true });
			columns.Add(new WebGridColumn() { Header = Translate.WebgridCreationDate, ColumnName = "CreationDate", CanSort = true });
			columns.Add(new WebGridColumn() { Header = Translate.WebgridChangedDate, ColumnName = "ChangedDate", CanSort = true });
			columns.Add(new WebGridColumn() { Header = Translate.WebgridPublishedDate, ColumnName = "PublishedDate", CanSort = true });
			columns.Add(new WebGridColumn() { Header = Translate.WebgridActive, ColumnName = "Active", CanSort = true, Style = "text-center", Format = (item) => { return new HtmlString("<i class='" + (item["Active"] > 0 ? "glyphicon glyphicon-ok" : "") + "'></i>"); } });
			columns.Add(new WebGridColumn() { Header = Translate.WebgridKeywords, ColumnName = "HasKeywords", CanSort = true, Style = "text-center", Format = (item) => { return new HtmlString("<i class='" + (item["HasKeyWords"] ? "glyphicon glyphicon-ok" : "") + "'></i>"); } });
			columns.Add(new WebGridColumn() { Header = Translate.WebgridMetaData, ColumnName = "HasMetadata", CanSort = true, Style = "text-center", Format = (item) => { return new HtmlString("<i class='" + (item["HasMetaData"] ? "glyphicon glyphicon-ok" : "") + "'></i>"); } });

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

		// GET: Article/Create
		[Authorize(Roles = "Admin,Editor,Manager,Moderator")]
		public ActionResult Create()
		{
			ViewBag.Title = String.Format(Translate.CreateButton, Translate.ArticleName);
			return View();
		}

		[Authorize(Roles = "Admin,Editor,Manager,Moderator,Contributors")]
		public ActionResult Change(int id)
		{
			ViewBag.Title = String.Format(Translate.ChangeButton, Translate.ArticleName);

			ArticleItem ArticleItem = ArticleClass.getArticle(id);

			return View("Create", ArticleItem);
		}
    }
}