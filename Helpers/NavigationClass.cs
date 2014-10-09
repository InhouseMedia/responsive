using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Responsive.Models;
using Responsive.Controllers;

namespace Responsive.Helpers
{
	public class NavigationItem
	{
		public NavigationItem()
		{
			ChildLocations = new HashSet<NavigationItem>();
		}

		public int Id { get; set; }
		public int ArticleId { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public string OnClick { get; set; }
		public double Priority { get; set; }
		public ICollection<Navigation_PublishLogs> PublishLogs { get; set; }
		public ICollection<NavigationItem> ChildLocations { get; set; }
	}
	

	public static class NavigationClass
	{
		public static NavigationItem currentNavigationItem { get; set; }
		public static List<NavigationItem> allNavigationItems { get; set; }

		private static List<NavigationItem> getNavigationItems(List<Navigation> navigation, string parentUrl = "/")
		{
			List<NavigationItem> result = new List<NavigationItem>();

			//var path = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
			string path = HttpContext.Current.Request.Url.AbsolutePath;

			using (ResponsiveContext db = new ResponsiveContext())
			{
				foreach (var item in navigation)
				{
					NavigationItem tempNav = getNavigationItem(item);
					List<Navigation> tempSub = db.Navigation.Where(
						x => 
						x.Parent_Id == item.Navigation_Id &&
						x.Navigation_PublishLogs.Count > 0 &&
						x.Active != 0
					).OrderBy(x => x.Level).ToList();

					string tempExtra = (parentUrl == "/") ? "" : "/";
					tempNav.Url = parentUrl + tempExtra + tempNav.Url;
					tempNav.ChildLocations = getNavigationItems(tempSub, tempNav.Url);
					result.Add(tempNav);

					if (tempNav.Url == path )
						currentNavigationItem = tempNav;
				}
			}

			return result;
		}

		private static NavigationItem getNavigationItem(Navigation item)
		{
			return new NavigationItem
			{
				Id = item.Navigation_Id,
				ArticleId = item.Article_Id,
				Priority = item.Priority,
				Title = item.Navigation_Content.FirstOrDefault(x => x.Navigation_Id == item.Navigation_Id).Title,
				Url = item.Navigation_Content.FirstOrDefault(x => x.Navigation_Id == item.Navigation_Id).Url,
				OnClick = item.Navigation_Content.FirstOrDefault(x => x.Navigation_Id == item.Navigation_Id).On_Click,
				PublishLogs = item.Navigation_PublishLogs,
				ChildLocations = { }
			};
		}

		public static List<NavigationItem> getNavigation(bool refresh = false) {

			if (allNavigationItems != null && !refresh)
				return allNavigationItems;

			allNavigationItems = null;

			using(ResponsiveContext db = new ResponsiveContext()){
				List<Navigation> navItems = db.Navigation.Where(
					x =>  
					x.Parent_Id == null && 
					x.Navigation_PublishLogs.Count > 0 &&
					x.Active != 0
				).OrderBy(x => x.Level).ToList();

				allNavigationItems = getNavigationItems(navItems);
			}
			return allNavigationItems;
		}

		public static List<tUrl> getSitemap(HttpRequestBase Request)
		{
			List<NavigationItem> allNavItems = null;
			List<tUrl> SitemapUrls = new List<tUrl>();

			using (ResponsiveContext db = new ResponsiveContext())
			{
				// Get data from database
				// TODO: should be cached
				List<Navigation> navigation = db.Navigation.Where(x => x.Active != 0 && x.Navigation_PublishLogs.Count > 0).OrderBy(x => x.Level).ToList();
				allNavItems = getNavigationItems(navigation);

				// Define domain of the website
				string currentDomain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host;

				foreach (NavigationItem navItem in allNavItems)
				{
					// Initialize variables
					int averageTime = 0;
					tChangeFreq changefreq = tChangeFreq.monthly;
					var changeFreqList = new List<double>();
					var navLogs = navItem.PublishLogs.ToArray();

					// Define the what the frequency is of changing de navigation structure by
					// determen the average of all publish dates
					// TODO:
					// The change frequency should be determent of the article itself instead of the navigation publish date
					for (int i = 0; navLogs.Length > i; i++) {
						int y = i;
						y++;
						var oldDate = navLogs[i].Published_Date;
						var newDate = (y < navLogs.Length) ? navLogs[y].Published_Date : DateTime.Today;
						changeFreqList.Add((newDate.Subtract(oldDate)).TotalDays);
					}

					if (changeFreqList.Count > 0)
						averageTime = (int)Math.Round(changeFreqList.Average());

					// Determen the changefrequency
					if (averageTime > 0 && averageTime < 1) {
						changefreq = tChangeFreq.always;
					} else if (averageTime >= 1 && averageTime < 3) {
						changefreq = tChangeFreq.daily;
					} else if (averageTime >= 3 && averageTime < 15) {
						changefreq = tChangeFreq.weekly;
					} else if (averageTime >= 15 && averageTime < 59) {
						changefreq = tChangeFreq.monthly;
					} else if (averageTime >= 59) {
						changefreq = tChangeFreq.yearly;
					}
					
					// Create the a new Url within the sitemap
					SitemapUrls.Add(new tUrl
						{
							loc = currentDomain + navItem.Url,
							lastmod = navItem.PublishLogs.OrderByDescending(x => x.Published_Date).FirstOrDefault().Published_Date.ToShortDateString(), //.OrderByDescending(x => x.Published_Date).ToShortDateString(),
							changefreq = changefreq,
							priority = (decimal)navItem.Priority,
							prioritySpecified = (navItem.Priority > 0),
							changefreqSpecified = (changeFreqList.Count() > 0)
						}
					);
					
				}
			}

			return SitemapUrls;
		}
	}
}