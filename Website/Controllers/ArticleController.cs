namespace Responsive.Controllers
{
	using System.Data.Entity;
	using System.Dynamic;
	using System.Linq;
	using System.Web.Mvc;

	using Library.Classes;
	using Library.Models;
	using Library.Setup;

	public class ArticleController : Controller
	{

		public ActionResult Index()
		{
			int ArticleId = NavigationClass.currentNavigationItem.ArticleId;
			ArticleItem ArticleItem = ArticleClass.getArticle(ArticleId, true);

			ViewBag.MetaTitle = ArticleItem.Metadata.Select(x => x.Meta_Title).FirstOrDefault();
			ViewBag.MetaDescription = ArticleItem.Metadata.Select(x => x.Meta_Description).FirstOrDefault();
			ViewBag.MetaKeywords = ArticleItem.Metadata.Select(x => x.Meta_Keywords).FirstOrDefault();

			ViewBag.Title = ArticleItem.Content.Select(x => x.Title).FirstOrDefault();
			ViewBag.Message = ArticleItem.Content.Select(x => x.Text).FirstOrDefault();

			//ViewBag.Content = ArticleItem.Content;

			ViewBag.NavigationUrl = "";

			return View(ArticleItem.Content);

		}

		// GET: Article - Text
		public ActionResult Text(Article_Content content)
		{
			
			return View(content);
		}
		
		// GET: Article - Video
		public ActionResult Video(Article_Content content)
		{
			
			var ctx = new DocumentEntities();
			
				ctx.DocumentsView.Load();
			
			dynamic mymodel = new ExpandoObject();
			mymodel.Content = content;
			mymodel.Data = ctx.DocumentsView.Local;
			return View(mymodel);

			//return View(content);
		}


		// GET: Article - Generate default data
		public ActionResult Generate(Article_Content content)
		{
			AutoFillDatabaseClass.GetScripts();

			return View(content);
		}
	}
}
