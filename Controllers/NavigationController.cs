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
        
        public class NavigationItem
        {
            public NavigationItem()
            {
                ChildLocations = new HashSet<NavigationItem>();
            } 

            public int Id { get; set; }
            public string Title { get; set; }
			public string Url { get; set; }
			public string OnClick { get; set; }
            public ICollection<NavigationItem> ChildLocations { get; set; }
        }
        
        // GET: Navigation
        public ActionResult Index()
        {
            List<Navigation> navigation = db.Navigation.Where(x => x.Active == 1).OrderBy(x => x.Level).ToList();

			List<NavigationItem> test = getNaviationItems(navigation);
			/*
			List<NavigationItem> test = navigation.Select(
				x => new NavigationItem {
					Id = x.Navigation_Id,
					Title = x.Url,
					Url = x.Url,
					OnClick = x.On_Click,
					ChildLocations = { }
				}
			).ToList();
*/
            return View(test);
        }

		private List<NavigationItem> getNaviationItems(List<Navigation> navigation) {
			List<NavigationItem> navItem = new List<NavigationItem>();
			foreach (Navigation item in navigation) {

				 //string[] level = item.Level.Split('.');


				navItem.Add(new NavigationItem
				{
					Id = item.Navigation_Id,
					Title = item.Url,
					Url = item.Url,
					OnClick = item.On_Click,
					ChildLocations = { }
				})
				;
			}
			return navItem;
		}

        // GET: Navigation/Sitemap
        public ActionResult Sitemap()
        {
            Response.ContentType = "application/xml";

            List<Navigation> allNavItems = null;
            List<tUrl> SitemapUrls = new List<tUrl>();

            using (var db = new ResponsiveContext())
            {
                // Get data from database
                // TODO: should be cached
                allNavItems = db.Navigation.Where(x => x.Active == 1).OrderBy(x => x.Level).ToList();

                // Define domain of the website
                string currentDomain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host;

                foreach (Navigation navItem in allNavItems)
                {
                    // Initialize variables
                    int averageTime = 0;
                    tChangeFreq changefreq = tChangeFreq.monthly;
                    var changeFreqList = new List<double>();
                    var navLogs = navItem.Navigation_PublishLogs.ToArray();

                    // Define the what the frequency is of changing de navigation structure by
                    // determen the average of all publish dates
                    // TODO:
                    // The change frequency should be determent of the article itself instead of the navigation publish date
                    for (var i = 0; navLogs.Length > i; i++) {
                        changeFreqList.Add((navLogs[i].Published_Date-navLogs[i++].Published_Date).TotalDays);
                    }

                    if(changeFreqList.Count > 0)
                         averageTime = (int)Math.Round(changeFreqList.Average());
                    
                    // Determen the changefrequency
                    if(averageTime > 0 && averageTime < 1) {
                        changefreq = tChangeFreq.always;
                    }else if (averageTime >= 1 && averageTime < 3) {
                        changefreq = tChangeFreq.daily;
                    }else if (averageTime >= 3 && averageTime < 15) {
                        changefreq = tChangeFreq.weekly;
                    }else if (averageTime >= 15 && averageTime < 59) {
                        changefreq = tChangeFreq.monthly;
                    }else if (averageTime >= 59) {
                        changefreq = tChangeFreq.yearly;
                    }

                    // Create the a new Url within the sitemap
                    SitemapUrls.Add(new tUrl
                        {
                            loc = currentDomain + "/" + navItem.Url,
                            lastmod = navItem.Creation_Date.ToShortDateString(),
                            changefreq = changefreq,
                            priority = (decimal)navItem.Priority,
                            prioritySpecified = (navItem.Priority > 0),
                            changefreqSpecified = (changeFreqList.Count() > 0)
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
