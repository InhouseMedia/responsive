namespace Responsive.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.Entity;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Web;
	using System.Web.Mvc;
	using System.Xml;
	using System.Xml.Serialization;
	using System.Xml.Schema;
	using Responsive.Helpers;
	using Responsive.Models;

	public class NavigationController : Controller
    {
		ResponsiveContext db = new ResponsiveContext();

        // GET: Navigation
        public ActionResult Index()
        {
			return View(NavigationClass.getNavigation());
        }

        // GET: Navigation/Sitemap
        public ActionResult Sitemap()
        {
            Response.ContentType = "application/xml";

			// Get all active navigationitems
			List<tUrl> sitemapUrls = NavigationClass.getSitemap(Request);

			// Create a XML output of a navigation Object
			var xmlx = new urlset { url = sitemapUrls };
            var serializer = new XmlSerializer(typeof(urlset));
            StringWriter textWriter = new StringWriter();
            serializer.Serialize(textWriter, xmlx);
            
            ViewBag.XmlData = textWriter;

            return View();
        }

        // GET: Navigation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navigation navigation = db.Navigation.Find(id);
            if (navigation == null)
            {
                return HttpNotFound();
            }
            return View(navigation);
        }

        // GET: Navigation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Navigation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NavigationId,Article_Id,Url,On_Click,Level,Priority,Active,Created_By,Creation_Date")] Navigation navigation)
        {
            if (ModelState.IsValid)
            {
                db.Navigation.Add(navigation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(navigation);
        }

        // GET: Navigation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navigation navigation = db.Navigation.Find(id);
            if (navigation == null)
            {
                return HttpNotFound();
            }
            return View(navigation);
        }

        // POST: Navigation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NavigationId,Article_Id,Url,On_Click,Level,Priority,Active,Created_By,Creation_Date")] Navigation navigation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(navigation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(navigation);
        }

        // GET: Navigation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navigation navigation = db.Navigation.Find(id);
            if (navigation == null)
            {
                return HttpNotFound();
            }
            return View(navigation);
        }

        // POST: Navigation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Navigation navigation = db.Navigation.Find(id);
            db.Navigation.Remove(navigation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
