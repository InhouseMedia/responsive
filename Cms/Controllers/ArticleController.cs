namespace Cms.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Web;
	using System.Web.Helpers;
	using System.Web.Mvc;

	using Library.Classes;
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
			var currentManagerRole = User.IsInRole("Manager");
			var currentModeratorRole = User.IsInRole("Moderator");
			var currentViewerRole = User.IsInRole("Viewer");

			List<ArticleListItem> articleList = ArticleClass.getArticleList();

			List<WebGridColumn> columns = new List<WebGridColumn>();
			//columns.Add(new WebGridColumn() { ColumnName = "Id", Header = "Id", CanSort = true });
			columns.Add(new WebGridColumn() { Header = Translate.WebgridTitle, ColumnName = "Title", CanSort = true });
			columns.Add(new WebGridColumn() { Header = Translate.WebgridCreationDate, ColumnName = "CreationDate", CanSort = true, Format = item => String.Format("{0:g}", item["CreationDate"]) });
			columns.Add(new WebGridColumn() { Header = Translate.WebgridChangedDate, ColumnName = "ChangedDate", CanSort = true, Format = item => String.Format("{0:g}", item["ChangedDate"]) });
			columns.Add(new WebGridColumn() { Header = Translate.WebgridPublishedDate, ColumnName = "PublishedDate", CanSort = true, Format = item => String.Format("{0:g}", item["PublishedDate"]) });
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
					bool disableCheckBox = (currentViewerRole || currentModeratorRole);
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
					//Pages with pageId smaller then 100 are system pages like the 404 error page
					bool disableCheckBox = ((!currentAdminRole && !currentManagerRole ) || item.Id < 100);
					return new HtmlString("<a href='/Article/Delete/" + item.Id + "' class='btn btn-default btn-xs" + (disableCheckBox ? " disabled" : "") + "'><i class='glyphicon glyphicon-trash'></i></a>");
				}
			});

			columns.Add(new WebGridColumn()
			{
				ColumnName = "Preview",
				Header = "",
				Style = "text-center",
				Format = (item) =>
				{		
					return new HtmlString("<a href='/Article/Preview/" + item.Id + "' class='btn btn-default btn-xs'><i class='glyphicon glyphicon-eye-open'></i></a>");
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

			return View("Create",  ArticleClass.getArticle(id, false));
		}
    }
}