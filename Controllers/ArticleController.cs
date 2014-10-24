namespace Responsive.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.Entity;
	using System.Dynamic;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Web;
	using System.Web.Mvc;
	using Responsive.Models;
	using Responsive.Helpers;

	public class ArticleController : Controller
	{

		public ActionResult Index()
		{
			int ArticleId = NavigationClass.currentNavigationItem.ArticleId;
			ArticleItem ArticleItem = ArticleClass.getArticle(ArticleId);

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
			
			/*
			var newDocument = new DocumentsView();
				newDocument.file_stream = System.IO.File.ReadAllBytes("C:\\Temp\\IMG_3401.JPG");
				newDocument.name = Path.GetFileName("somefile.txt");

				var ctx1 = new ResponsiveDocuments();
					ctx1.DocumentsView.Add(newDocument);
					ctx1.SaveChanges();
			*/
			var ctx = new ResponsiveDocuments();
			
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
