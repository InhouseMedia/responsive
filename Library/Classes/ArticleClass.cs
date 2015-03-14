namespace Library.Classes
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Microsoft.AspNet.Identity.Owin;

	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Data.Entity;
	using System.Linq;
	using System.Web.Mvc;

	using Library.Models;
	using Library.Resources;

	
	public class ArticleItem
	{
		public int Id { get; set; }
		public int Active { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
		[Display(Name = "WebgridCreationDate", ResourceType = typeof(Translate))]
		public DateTime CreationDate { get; set; }
		[Display(Name = "WebgridCreatedBy", ResourceType = typeof(Translate))]
		public string CreatedBy { get; set; }
		public IEnumerable<Article_Content> Content { get; set; }
		public Article_Metadata Metadata { get; set; }
		public IEnumerable<Article_PublishLogs> PublishLogs { get; set; }
		public IEnumerable<Article_ChangeLogs> ChangeLogs { get; set; }
		public AspNetUsers AspNetUsers { get; set; }
	}

	public class ArticleListItem
	{
		public int Id { get; set; }
		public int Active { get; set; }
		public string Title { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
		public DateTime CreationDate { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
		public DateTime PublishedDate { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
		public DateTime ChangedDate { get; set; }
		public string PublishedBy { get; set; }
		public string ChangedBy { get; set; }
		public bool HasMetaData { get; set; }
		public bool HasKeyWords { get; set; }
	}

	public class ArticleClass
	{
		public static ArticleItem currentArticleItem { get; set; }

		[Authorize]
		public static List<ArticleListItem> getArticleList()
		{
			List<ArticleListItem> ArticleList = new List<ArticleListItem>();
			using (LibraryEntities db = new LibraryEntities())
			{
				List<Article> tempArticles = db.Article
					.Where(b => b.Article_Content.Any(d => !d.Controller.Equals("File")) || b.Article_Content.Count == 0)
					.OrderByDescending(a => a.Article_Id)
					.ToList();

				foreach (Article item in tempArticles)
				{
					ArticleList.Add(getArticleListItem(item));
				}
			}
			return ArticleList;
		}

		public static ArticleItem getArticle(int Article_Id, bool ShowErrorPage = false)
		{
			// When retreiving the data for changing this article in the CMS you should not
			// return the error page. So in that case the error page is disabled:
			int errorPage = (ShowErrorPage) ? 10 : -1;

			// Check if the article that you've requested exsist. 
			// Otherwise show an error page. (By default it should be article_id : 10)
			// Todo: article 10 should trigger a Status Code:404 Not Found in the source.
			// !!!Coution!!! the page should also trigger this when the page is already in cache. 
			// So caching should be turned off when serving a page not found.
			using (LibraryEntities db = new LibraryEntities())
			{
				db.Configuration.LazyLoadingEnabled = false;

				Article tempArticle = db.Article
					.Include("Article_PublishLogs.AspNetUsers")
					.Include("Article_ChangeLogs.AspNetUsers")
					.Include("Article_Content.AspNetUsers")
					.Include("AspNetUsers")
					.Where(a => (a.Article_Id == Article_Id || a.Article_Id == errorPage) /*&& a.Active > 0*/)
					.OrderByDescending(x => x.Article_Id)
					.FirstOrDefault();

					currentArticleItem = getArticleItem(tempArticle);

				return currentArticleItem;
			}
		}

		private static ArticleItem getArticleItem(Article item)
		{
			return new ArticleItem
			{
				Id = item.Article_Id,
				Active = item.Active,
				CreationDate = item.Creation_Date,
				Content = item.Article_Content.OrderBy(x => x.Level).ToList(),
				Metadata = item.Article_Metadata.FirstOrDefault(),
				PublishLogs = item.Article_PublishLogs,
				AspNetUsers = item.AspNetUsers,
				//PublishLogs = getPublishLogs(item.Article_PublishLogs),
				ChangeLogs = item.Article_ChangeLogs
			};
		}


		private static ArticleListItem getArticleListItem(Article item)
		{
			return new ArticleListItem
			{
				Id = item.Article_Id,
				Active = item.Active,
				Title = item.Article_Content.Select(x => x.Title).FirstOrDefault(),
				CreationDate = item.Creation_Date,
				PublishedDate = item.Article_PublishLogs.Select(x => x.Published_Date).OrderByDescending(o => o).FirstOrDefault(),
				ChangedDate = item.Article_ChangeLogs.Select(x => x.Changed_Date).OrderByDescending(o => o).FirstOrDefault(),
				HasKeyWords = item.Article_Metadata.Select(x => x.Meta_Keywords != null && x.Meta_Keywords != "").FirstOrDefault(),
				HasMetaData = item.Article_Metadata.Select(x => x.Meta_Description != null && x.Meta_Description != "").FirstOrDefault()
			};
		}

		public static void setArticle(ArticleItem item){

			var changed = new Article() { 
				Article_Id = item.Id,
				Active = (byte) item.Active,
				//Article_Metadata = ,
				//Article_PublishLogs = (ICollection<Article_PublishLogs>) item.PublishLogs
			};

			using (LibraryEntities db = new LibraryEntities())
			{
				db.Configuration.LazyLoadingEnabled = false;
				
				db.Entry(changed).State = EntityState.Modified;

			}
		}
	}
}