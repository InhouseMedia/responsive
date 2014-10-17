namespace Responsive.Helpers
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Web;
	using System.Web.Hosting;
	using System.Web.Script.Serialization;
	//using AutoMapper;
	using Responsive.Models;
	//using System.Reflection;
	using Newtonsoft.Json.Linq;
	//using Omu.ValueInjecter;

	public class ConfigClass
	{

		private const string JsonConfigFile = "~/App_Data/Config.json";
		public static ConfigObject Settings { get; set; }

		private static Dictionary<string, object> createConfigObject(string jsonString)
		{
			//string jsonString = File.ReadAllText(HostingEnvironment.MapPath(file));
			var tmp = new JavaScriptSerializer();
			return tmp.Deserialize<Dictionary<string, object>>(jsonString);
		}

		public static void setConfig()
		{
			// Get default settings from JSON file
			string jsonDefaultConfig = File.ReadAllText(HostingEnvironment.MapPath(JsonConfigFile));

			string jsonSiteConfig = "";

			// Get website specific Config
			using (ResponsiveContext db = new ResponsiveContext())
			{
				jsonSiteConfig = db.Config.Select(x => x.Data).FirstOrDefault().ToString();
			}

			// Parse Json file into JObject to prepare for mergin
			JObject defaultJsonObject = JObject.Parse(jsonDefaultConfig);
			JObject siteJsonObject = JObject.Parse(jsonSiteConfig);

			// Merge defaultConfig with siteConfig
			Merge(defaultJsonObject, siteJsonObject);

			// Place Merged Json object into Config Class
			ConfigObject Settings = defaultJsonObject.ToObject<ConfigObject>();
		}

		private static void Merge(JObject receiver, JObject donor)
		{
			foreach (var property in donor)
			{
				JObject receiverValue = receiver[property.Key] as JObject;
				JObject donorValue = property.Value as JObject;
				if (receiverValue != null && donorValue != null)
					Merge(receiverValue, donorValue);
				else
					receiver[property.Key] = property.Value;
			}
		}
		
		/*
		private static void getSettings(Dictionary<string, object> siteItems, Dictionary<string, object> defaultItems)
		{
			foreach (var item in siteItems)
			{
				if (!defaultItems.ContainsKey(item.Key)) continue;

				if (item.Value.GetType() == typeof(Dictionary<string, object>))
				{
					Dictionary<string, object> tempItem = (Dictionary<string, object>)item.Value;
					Dictionary<string, object> tempDefault = (Dictionary<string, object>)defaultItems[item.Key];
					getSettings(tempItem, tempDefault);
				}
				else
				{
					defaultItems[item.Key] = item.Value;
				}
			}
		}
		*/




		public class ConfigObject
		{
			public Language language { get; set; }
			public Searchengines searchEngines { get; set; }
			public Controllers controllers { get; set; }
		}

		public class Language
		{
			public string[] locale { get; set; }
		}

		public class Searchengines
		{
			public string googleverification { get; set; }
			public bool googleTranslation { get; set; }
			public bool socialMediaTags { get; set; }
			public string websiteType { get; set; }
			public string author { get; set; }
		}

		public class Controllers
		{
			public Standard standard { get; set; }
			public Article article { get; set; }
			public Navigation navigation { get; set; }
		}

		public class Standard
		{
			public bool maintenance { get; set; }
		}

		public class Article
		{
			public bool active { get; set; }
			public bool superAdmin { get; set; }
			public string[] actions { get; set; }
		}

		public class Navigation
		{
			public bool active { get; set; }
			public bool superAdmin { get; set; }
			public int maxLength { get; set; }
			public Placement placement { get; set; }
		}

		public class Placement
		{
			public int Disabled { get; set; }
			public int Allnavigations { get; set; }
			public int Topnavigationonly { get; set; }
			public int Leftnavigationonly { get; set; }
			public int Bottomnavigationonly { get; set; }
			public int Allexcepttopnavigation { get; set; }
			public int _301Redirect { get; set; }
		}

	}


	
	

}