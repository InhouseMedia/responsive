using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Responsive.Models;
using Responsive.Helpers;


namespace Responsive.Controllers
{
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

			return View(content);
		}


		// GET: Article - Generate default data
		public ActionResult Generate(Article_Content content)
		{
			AutoFillDatabaseClass.GetScripts();

			return View(content);
		}
	}
}
