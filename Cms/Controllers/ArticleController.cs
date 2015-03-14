namespace Cms.Controllers
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Microsoft.AspNet.Identity.Owin;
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Web;
	using System.Web.Helpers;
	using System.Web.Mvc;
	using System.Threading.Tasks;

	using Library.Classes;
	using Library.Models;
	using Library.Resources;

	[Authorize]
	public class ArticleController : Controller
    {
		LibraryEntities context = new LibraryEntities();

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
			columns.Add(new WebGridColumn() { Header = Translate.WebgridActive, ColumnName = "Active", CanSort = true, Style = "text-center", Format = (item) => { return new HtmlString("<i class='" + (item["Active"] > 0 ? item["Active"] == 2 ? "glyphicon glyphicon-ok disabled" : "glyphicon glyphicon-ok" : "") + "'></i>"); } });
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

			var emptyMetadata = new HashSet<Article_Metadata>();
				emptyMetadata.Add(new Article_Metadata());

			var viewArticle = new Article() {
				Article_Metadata = emptyMetadata
			};

			return View(viewArticle);
		}

        // POST: /Account/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Change(Article model, string returnUrl)
		{
			if (ModelState.IsValid)
			{	
				// Get previous saved data for comparison reasons.
				Article currentArticle;
				using (LibraryEntities db = new LibraryEntities()){
					 currentArticle = db.Article
						.Include("Article_PublishLogs")
						.Include("Article_ChangeLogs")
						.Include("Article_Metadata")
						.Include("Article_Content")
						.Where(a => a.Article_Id == model.Article_Id)
						.First();
				}

				// Add extra info to the changed model that it needs to save and 
				// we don't want to be visible as hidden input fields on the page
				model.Created_By = currentArticle.Created_By;
				model.Article_PublishLogs.First().Article_Id = model.Article_Id;
				model.Article_PublishLogs.First().Published_By = User.Identity.GetUserId();
				model.Article_Metadata.First().Article_Id = model.Article_Id;

				// Add Change log
				Article_ChangeLogs changeLog = new Article_ChangeLogs()
				{
					Article_Id = model.Article_Id,
					Changed_By = User.Identity.GetUserId(),
					Changed_Date = DateTime.Now
				};

				model.Article_ChangeLogs.Add(changeLog);

				using (LibraryEntities db = new LibraryEntities())
				{					
					// Change Article items
					db.Entry(model).State = EntityState.Modified;

					// Add Change data
					db.Entry(model.Article_ChangeLogs.First()).State = EntityState.Added;

					DateTime oldPublishDate = (currentArticle.Article_PublishLogs.Count > 0)
						?currentArticle.Article_PublishLogs.OrderByDescending(p => p.Published_Date).FirstOrDefault().Published_Date
					: new DateTime();
					DateTime newPublishDate = model.Article_PublishLogs.First().Published_Date;

					// Define New publishDate when there is no PublishDate filled
					if (newPublishDate == new DateTime() && oldPublishDate < DateTime.Now) {
						model.Article_PublishLogs.FirstOrDefault().Published_Date = DateTime.Now;
						db.Entry(model.Article_PublishLogs.First()).State = EntityState.Added;
					}

					// Add new PublishDate that's been filled-in when changing the page
					if (newPublishDate != new DateTime() && newPublishDate > oldPublishDate) {
						db.Entry(model.Article_PublishLogs.First()).State = EntityState.Added;
					}

					// Add or change metadata
					bool hasMedia = (model.Article_Metadata.First().Id > 0);
					db.Entry(model.Article_Metadata.First()).State = (hasMedia) ? EntityState.Modified : EntityState.Added;

					foreach (Article_Content item in model.Article_Content) {
						db.Entry(item).State = (item.Id == 0) ? EntityState.Added : EntityState.Modified;
					}

					db.SaveChanges();
				}

				return RedirectToAction("List");
			}

			return View(model);

		}

		[Authorize(Roles = "Admin,Editor,Manager,Moderator,Contributors")]
		public ActionResult Change(int id)
		{
			ViewBag.Title = String.Format(Translate.ChangeButton, Translate.ArticleName);

			using (LibraryEntities db = new LibraryEntities())
			{
				db.Configuration.LazyLoadingEnabled = false;
				var viewPublish = db.Article_PublishLogs
					.Include("AspNetUsers")
					.Where(a => a.Article_Id == id)
					.OrderByDescending(p => p.Published_Date).ToList();

				var viewContent = db.Article_Content
					.Include("AspNetUsers")
					.Where(a => a.Article_Id == id)
					.OrderBy(p => p.Level).ToList();

				Article viewArticle = db.Article
					.Include("Article_Metadata")
					.Include("AspNetUsers")
					.Where(a => a.Article_Id == id)
					//.OrderByDescending(x => x.Article_Id)
					.FirstOrDefault();

				viewArticle.Article_PublishLogs = viewPublish;
				viewArticle.Article_Content = viewContent;

				// When there is no Metadata we need to add an empty item in it to show the EditorTemplate
				if (viewArticle.Article_Metadata.Count == 0) { 
					var emptyMetadata = new HashSet<Article_Metadata>();
						emptyMetadata.Add(new Article_Metadata() { Article_Id = viewArticle.Article_Id});

						viewArticle.Article_Metadata = emptyMetadata;
				}
				/*
				if (viewArticle.Article_PublishLogs.Count == 0)
				{
					var emptyPublishLog = new HashSet<Article_PublishLogs>();
						emptyPublishLog.Add(new Article_PublishLogs() { Article_Id = viewArticle.Article_Id });

						viewArticle.Article_PublishLogs = emptyPublishLog;

				}
				*/
				return View("Create", viewArticle);
			}
		}

		// GET: Article - Text
		public ActionResult Text(Article_Content content)
		{
			if (content.Id == 0) content.Id = new Random().Next(1000) * 1000000;

			return View(content);
		}

		// GET: Article - Video
		public ActionResult Video(Article_Content content)
		{
			if (content.Id == 0) content.Id = new Random().Next(1000);

			return View(content);
		}

		// GET: Article - E404
		public ActionResult E404(Article_Content content)
		{
			if (content.Id == 0) content.Id = new Random().Next(1000);

			return View("Text",content);
		}

    }
}