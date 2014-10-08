using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Responsive.Filters;
using Responsive.Models;
using Responsive.Helpers;

namespace Responsive.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			//AutoFillDatabaseClass.GetScripts();

			
			ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
			int ArticleId = NavigationClass.currentNavigationItem.ArticleId;
			ArticleItem ArticleItem = ArticleClass.getArticle(ArticleId);

			ViewBag.MetaTitle = ArticleItem.Metadata.Select(x => x.Meta_Title).FirstOrDefault();
			ViewBag.MetaDescription = ArticleItem.Metadata.Select(x => x.Meta_Description).FirstOrDefault();
			ViewBag.MetaKeywords = ArticleItem.Metadata.Select(x => x.Meta_Keywords).FirstOrDefault();

			ViewBag.Title = ArticleItem.Content.Select(x => x.Title).FirstOrDefault();
			ViewBag.Message = ArticleItem.Content.Select(x => x.Text).FirstOrDefault();

			ViewBag.NavigationUrl = "";

            return View();
           
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
