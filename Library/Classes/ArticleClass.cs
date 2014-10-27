namespace Library.Classes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
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

	public class ArticleClass
	{
		public static ArticleItem currentArticleItem { get; set; }

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
	}
}