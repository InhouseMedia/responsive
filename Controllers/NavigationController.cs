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
using Responsive.Models;

namespace Responsive.Controllers
{
    public class NavigationController : Controller
    {
        private ResponsiveContext db = new ResponsiveContext();

        // GET: Navigation
        public ActionResult Index()
        {
            return View(db.Navigation.ToList());
        }

        // GET: Navigation/Sitemap
        public ActionResult Sitemap()
        {
            Response.ContentType = "application/xml";

            List<Navigation> xNewItems = null;
            List<tUrl> SitemapUrls = new List<tUrl>();

            using (var db = new ResponsiveContext())
            {
                xNewItems = db.Navigation.Where(x => x.Active == 1).OrderBy(x => x.Level).ToList();

                string currentDomain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host;

                foreach (Navigation item in xNewItems)
                {
                    tChangeFreq changefreq = tChangeFreq.monthly;

                    if (item.Navigation_PublishLogs.Count() > 0)
                    {
                        // Define the what the frequency is of changing de navigation structure
                        // (should be article lateron)
                        var allPublishDate = item.Navigation_PublishLogs.ToArray();
                        Array.Reverse(allPublishDate);
                        
                        var lastPublishDate = allPublishDate.First().Published_Date;
                        var prevPublishDate = allPublishDate[1].Published_Date;

                        var averageTime = (lastPublishDate - prevPublishDate).TotalDays;

                        if(averageTime < 1) {
                            changefreq = tChangeFreq.always;
                        }else if (averageTime == 1 && averageTime < 3) {
                            changefreq = tChangeFreq.daily;
                        }else if (averageTime > 3 && averageTime < 15) {
                            changefreq = tChangeFreq.weekly;
                        }else if (averageTime > 29 && averageTime < 59) {
                            changefreq = tChangeFreq.monthly;
                        }else if (averageTime > 59) {
                            changefreq = tChangeFreq.yearly;
                        }
                    }

                    // Create the a new Url within the sitemap
                    SitemapUrls.Add(new tUrl
                        {
                            loc = currentDomain + "/" + item.Url,
                            lastmod = item.Creation_Date.ToShortDateString(),
                            changefreq = changefreq,
                            priority = (decimal)item.Priority,
                            prioritySpecified = (item.Priority > 0),
                            changefreqSpecified = (item.Navigation_PublishLogs.Count() > 0)
                        }
                    );
                }
            }

            var xmlx = new urlset { url = SitemapUrls };
           
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
