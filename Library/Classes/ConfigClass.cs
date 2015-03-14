namespace Library.Classes
{
	using System.IO;
	using System.Linq;
	using System.Web.Hosting;
	using Newtonsoft.Json.Linq;
	using Library.Models;

	public class ConfigClass
	{
		//private const string JsonConfigFile = "~/App_Data/Config.json";
		private const string JsonConfigFile = "Library\\Config\\Config.json";
		public static ConfigObject Settings { get; set; }

		public static void setConfig()
		{
			// Get default settings from JSON file
			string path = Path.Combine(new DirectoryInfo(HostingEnvironment.MapPath("~/")).Parent.FullName, @JsonConfigFile);
			string jsonDefaultConfig = File.ReadAllText(path);

			string jsonSiteConfig = "{}";

			// Get website specific Config
			using (LibraryEntities db = new LibraryEntities())
			{
				var tempConfig = db.Config.Select(x => x.Data).FirstOrDefault();
				jsonSiteConfig = (tempConfig ?? null).ToString();
			}

			// Parse Json file into JObject to prepare for mergin
			JObject defaultJsonObject = JObject.Parse(jsonDefaultConfig);
			JObject siteJsonObject = JObject.Parse(jsonSiteConfig);

			// Merge defaultConfig with siteConfig
			Merge(defaultJsonObject, siteJsonObject);

			// Place Merged Json object into Config Class
			Settings = defaultJsonObject.ToObject<ConfigObject>();
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


		public class ConfigObject
		{
			public Language language { get; set; }
			public Searchengines searchEngines { get; set; }
			public Socialmedia socialMedia { get; set; }
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
			public string websiteType { get; set; }
			public string author { get; set; }
			public string generator { get; set; }
			public visibility visibility { get; set; }
		}

		public class visibility 
		{
			public int PageVisibilityPublic { get; set; }
			public int PageVisibilityLimited { get; set; }
			public int PageVisibilityBlocked { get; set; }
		}

		public class Socialmedia
		{
			public bool socialMediaTags { get; set; }
			public Media media { get; set; }
		}

		public class Media
		{
			public string facebook { get; set; }
			public string twitter { get; set; }
			public string googleplus { get; set; }
			public string linkedin { get; set; }
			public string youtube { get; set; }
			public string pinterest { get; set; }
		}

		public class Controllers
		{
			public Standard standard { get; set; }
			public Article article { get; set; }
			public Navigation navigation { get; set; }
		}

		public class Standard
		{
			public bool active { get; set; }
			public bool admin { get; set; }
			public bool maintenance { get; set; }
		}

		public class Article
		{
			public bool active { get; set; }
			public bool admin { get; set; }
			public string[] tools { get; set; }
			public string[] actions { get; set; }
		}

		public class Navigation
		{
			public bool active { get; set; }
			public bool admin { get; set; }
			public int maxLength { get; set; }
			public Placement placement { get; set; }
		}

		public class Placement
		{
			public int Disabled { get; set; }
			public int AllNavigations { get; set; }
			public int TopNavigationOnly { get; set; }
			public int LeftNavigationOnly { get; set; }
			public int BottomNavigationOnly { get; set; }
			public int AllExceptTopNavigation { get; set; }
			public int _301Redirect { get; set; }
		}
	}
}