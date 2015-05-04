namespace Cms
{
	using System.Web;
	using System.Web.Optimization;
	using System.Linq;
	using ExtensionMethods;
	using Newtonsoft.Json;

	using Library.Resources;

	public class CustomBundle : Bundle
	{
		public CustomBundle(string virtualPath)
			: base(virtualPath, new CustomTransform())
		{
		}
	}

	public class CustomTransform : IBundleTransform
	{
		public void Process(BundleContext context, BundleResponse response)
		{
			response.Content = string.Format(";var Translate = {0};", TranslateScript());
			response.ContentType = "text/javascript";
			response.Cacheability = HttpCacheability.Server;
		}

		private string TranslateScript()
		{
			return JsonConvert.SerializeObject(
				typeof(Translate)
				.GetProperties()
				.Where(p => !p.Name.IsLikeAny("ResourceManager", "Culture")) // Skip the properties you don't need on the client side.
				.ToDictionary(p => p.Name, p => p.GetValue(null) as string)
				 );
		}
	}

	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{

			// Set EnableOptimizations to false for debugging. For more information,
			// visit http://go.microsoft.com/fwlink/?LinkId=301862
			//BundleTable.EnableOptimizations = true;

			bundles.Add(new CustomBundle("~/bundles/resources"));
			
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/jquery-ui-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(		
						"~/Scripts/jquery.unobtrusive*",  
						"~/Scripts/jquery.validate*",
						"~/Scripts/Site/validations.js"
						));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js",
					  "~/Scripts/moment.js",
					  "~/Scripts/Locale/*.js",
					  "~/Scripts/bootstrap-datetimepicker.js",
					  "~/Scripts/bootstrap-slider.js",
					  "~/Scripts/Site/common.js"));

			bundles.Add(new ScriptBundle("~/bundles/article").Include(
					"~/Scripts/Site/article.js",
					"~/Scripts/dropzone/dropzone.js"));

			bundles.Add(new StyleBundle("~/bundles/articlecss").Include(
					"~/Scripts/dropzone/css/basic.css",
					"~/Scripts/dropzone/css/dropzone.css"));

			bundles.Add(new ScriptBundle("~/bundles/roles").Include("~/Scripts/Site/roles.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));
			
			bundles.Add(new LessBundle("~/bundles/css").Include(
					"~/Content/bootstrap-datetimepicker/bootstrap-datetimepicker.less",
					"~/Content/bootstrap-slider/bootstrap-slider.less",
					"~/Content/Index.less")
					);
			
		}
	}
}

