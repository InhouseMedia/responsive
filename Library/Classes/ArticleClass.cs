namespace Library.Classes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;
	//using System.Web;

	using Library.Models;

	public class ArticleItem
	{
		public int Id { get; set; }
		public int Active { get; set; }
		public DateTime CreationDate { get; set; }
		public ICollection<Article_Content> Content { get; set; }
		public ICollection<Article_Metadata> Metadata { get; set; }
		public ICollection<Article_PublishLogs> PublishLogs { get; set; }
		public ICollection<Article_ChangeLogs> ChangeLogs { get; set; }
	}

	public class ArticleListItem
	{
		public int Id { get; set; }
		public int Active { get; set; }
		public string Title { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime PublishedDate { get; set; }
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
			using (ResponsiveContext db = new ResponsiveContext())
			{
				List<Article> tempArticles = db.Article.OrderByDescending(x => x.Article_Id).ToList();
				foreach (Article item in tempArticles) {
					ArticleList.Add(getArticleListItem(item));
				}
			}
			return ArticleList;
		}

		public static ArticleItem getArticle(int Article_Id) {

			//ArticleItem ArticleItem = null;

			using (ResponsiveContext db = new ResponsiveContext())
			{
				List<Article> tempArticle = db.Article.Where(
					x => 
					(x.Article_Id == Article_Id || x.Article_Id == 10) && 
					x.Active != 0
				).OrderByDescending(x => x.Article_Id).ToList();


				currentArticleItem = getArticleItem(tempArticle.First());
			}
			return currentArticleItem;
		}
		
		private static ArticleItem getArticleItem(Article item)
		{
			return new ArticleItem
			{
				Id = item.Article_Id,
				Active = item.Active,
				CreationDate = item.Creation_Date,
				Content = item.Article_Content.ToList(),
				Metadata = item.Article_Metadata,
				PublishLogs = item.Article_PublishLogs,
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
				PublishedDate = item.Article_PublishLogs.Select(x => x.Published_Date).FirstOrDefault(),
				ChangedDate = item.Article_ChangeLogs.Select(x => x.Changed_Date).FirstOrDefault(),
				HasKeyWords = item.Article_Metadata.Select(x => x.Meta_Keywords != "").FirstOrDefault(),
				HasMetaData = item.Article_Metadata.Select(x => x.Meta_Description != "").FirstOrDefault()
			};
		}

	}
}